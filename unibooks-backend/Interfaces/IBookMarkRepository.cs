using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibooks_backend.Models;

namespace unibooks_backend.Interfaces
{
    public interface IBookMarkRepository
    {
        Task<List<Advert>> GetUserBookmarksAsync(APPUser user);
        Task<BookMark> CreateAsync(BookMark bookMark);
    }
}