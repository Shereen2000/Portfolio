using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using unibooks_backend.Models;

namespace unibooks_backend.Interfaces
{
    public interface IAdvertRepository
    {
        Task<Advert?> GetByIdAsync(int id);

       Task<List<Advert>> GetAllAsync(AdvertQueryObject query);

        Task<Advert> CreateAsync(Advert advertModel, List<IFormFile> bookpics);

        Task<Advert?> DeleteAsync(int id);
    }
}