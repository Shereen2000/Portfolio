using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using Microsoft.EntityFrameworkCore;
using unibooks_backend.Data;
using unibooks_backend.Dtos.Book;
using unibooks_backend.Interfaces;
using unibooks_backend.Models;

namespace unibooks_backend.Repositories
{
    public class BookmarkRepository : IBookMarkRepository
    {
        private readonly ApplicationDBContext _context;
        public BookmarkRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<BookMark> CreateAsync(BookMark bookMark)
        {
            await _context.bookMarks.AddAsync(bookMark);
            await _context.SaveChangesAsync();
            return bookMark;
        }

        public async Task<List<Advert>> GetUserBookmarksAsync(APPUser user)
        {
             return await _context.bookMarks.Where(u => u.AppUserId == user.Id).Select(advert => new Advert
             {
                Id = advert.AdvertId,
                Price = advert.Advert.Price,
                Condition = advert.Advert.Condition,
                CreatedOn = advert.Advert.CreatedOn,
                BookId = advert.Advert.BookId,
                Book = advert.Advert.Book,
                BookImages = advert.Advert.BookImages
             }).ToListAsync();
        }
    }
}