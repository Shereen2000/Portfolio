using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using unibooks_backend.Data;
using unibooks_backend.Interfaces;
using unibooks_backend.Models;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Mvc.Routing;
using Helpers;

namespace unibooks_backend.Repositories
{
    public class AdvertRepository: IAdvertRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IAmazonS3 _s3Client;

        private IConfiguration _config;

        public AdvertRepository(ApplicationDBContext context, IConfiguration config)
        {
             _context = context;
             _config = config;
            _s3Client = new AmazonS3Client(_config["awsAccessKeyId"], _config["awsSecretAccessKey"]);
        }

        public async Task<Advert> CreateAsync(Advert advertModel, List<IFormFile> bookpics)
        {

             await _context.Adverts.AddAsync(advertModel);
             await _context.SaveChangesAsync();

            foreach(IFormFile pic in bookpics)
            {
                var key = $"{advertModel.Id}_{pic.FileName}_{DateTime.Now.Ticks}";
                
                var request = new PutObjectRequest
                {
                    BucketName = "s3-unibooks-bucket",
                    Key = key,
                    InputStream = pic.OpenReadStream(),
                    ContentType = pic.ContentType,
                };

                await _s3Client.PutObjectAsync(request);
                var publicUrl = GeneratePublicUrl("s3-unibooks-bucket",key);

                BookImage bookImageModel = new BookImage 
                { 
                    Url= publicUrl,
                    AdvertId = advertModel.Id
                };

                await _context.BookImages.AddAsync(bookImageModel);    
            }

                await _context.SaveChangesAsync();

            return advertModel;
        }

         private string GeneratePublicUrl(string bucketName, string key)
         {
            var region = "af-south-1"; // Replace with your S3 bucket region
            var url = $"https://{bucketName}.s3.{region}.amazonaws.com/{key}";

            return url;
         }

          public async Task<Advert?> GetByIdAsync(int id)
          {
               return await _context.Adverts
               .Include(b => b.Book)
               .Include(b => b.BookImages)
               .FirstOrDefaultAsync(a => a.Id == id);
          }

        public async Task<Advert> DeleteAsync(int id)
        {
            var advertmodel = await _context.Adverts.FirstOrDefaultAsync(a => a.Id == id);

            if(advertmodel == null)
            {
                return null;
            }

            _context.Adverts.Remove(advertmodel);
            await _context.SaveChangesAsync();

            return advertmodel;
        }

        public async Task<List<Advert>> GetAllAsync(AdvertQueryObject query)
        {
             var adverts = _context.Adverts.Include(b => b.Book).Include(b => b.BookImages).AsQueryable();

              if(!string.IsNullOrEmpty(query.Title))
            {
                adverts = adverts.Where(b => b.Book.Title.Contains(query.Title));
            }
             if(!string.IsNullOrEmpty(query.Publisher))
            {
                adverts = adverts.Where(b => b.Book.Publisher.Contains(query.Publisher));
            }
            if(!string.IsNullOrEmpty(query.SortBy))
            {
               if(query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
               {
                    adverts = query.IsDescending? adverts.OrderByDescending(b => b.Book.Title): adverts.OrderBy(b => b.Book.Title);
               }
            }

            return await adverts.ToListAsync();
               
        }
    }
}