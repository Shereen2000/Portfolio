using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using unibooks_backend.Dtos.Book;
using unibooks_backend.Interfaces;
using unibooks_backend.Models;
using Microsoft.EntityFrameworkCore;
using unibooks_backend.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unibooks_backend.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IAmazonS3 _s3Client;

        private IConfiguration _config;

        public BookRepository(ApplicationDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
             _s3Client = new AmazonS3Client(_config["awsAccessKeyId"], _config["awsSecretAccessKey"]); //enter own aws keys
           
        }
        public async Task<Book> CreateAsync(Book bookmodel, IFormFile coverpage)
        {
            var key =  $"{bookmodel.Id}_{coverpage.FileName}_{DateTime.Now.Ticks}";
            var request = new PutObjectRequest
            {
                BucketName = "s3-unibooks-bucket",
                Key = key,
                InputStream = coverpage.OpenReadStream(),
                ContentType = coverpage.ContentType,

            };

            await _s3Client.PutObjectAsync(request);
            var publicUrl = GeneratePublicUrl("s3-unibooks-bucket",key);

            bookmodel.CoverUrl = publicUrl;

            await _context.Book.AddAsync(bookmodel);
            await _context.SaveChangesAsync();

            return bookmodel;
            
        }

        private string GeneratePublicUrl(string bucketName, string key)
        {
            var region = "af-south-1"; // Replace with your S3 bucket region
            var url = $"https://{bucketName}.s3.{region}.amazonaws.com/{key}";

            return url;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Book.Include(a => a.Adverts).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Book>> GetAllAsync(BookQueryObject query)
        {
            var books =  _context.Book.Include(a => a.Adverts).AsQueryable();

            if(!string.IsNullOrEmpty(query.Title))
            {
                books = books.Where(b => b.Title.Contains(query.Title));
            }
             if(!string.IsNullOrEmpty(query.Publisher))
            {
                books = books.Where(b => b.Publisher.Contains(query.Publisher));
            }
            if(!string.IsNullOrEmpty(query.SortBy))
            {
               if(query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
               {
                    books = query.IsDescending? books.OrderByDescending(b => b.Title): books.OrderBy(b => b.Title);
               }
            }

            return await books.ToListAsync();
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookmodel = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);

            if(bookmodel == null)
            {
                return null;
            }

            _context.Book.Remove(bookmodel);

            await _context.SaveChangesAsync();
            
            return bookmodel;
        }

        
        public async Task<Book> GetByISBNAsync(string isbn)
        {
            return await _context.Book.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }


        public Task<bool> BookExist(int id)
        {
            return _context.Book.AnyAsync(b => b.Id == id);
        }

        public Task<bool> BookExistByISBN(string isbn)
        {
             return _context.Book.AnyAsync(b => b.ISBN == isbn);
        }


        public Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto)
        {
            throw new NotImplementedException();
        }
    }
}