using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using unibooks_backend.Dtos.Book;
using unibooks_backend.Models;

namespace unibooks_backend.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(BookQueryObject query);
        Task<Book?> GetByIdAsync(int id); // firstordefault can return null that why the ?
        Task<Book> CreateAsync(Book bookmode, IFormFile coverpage);
        Task<Book> GetByISBNAsync(string isbn);
        Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto);
        Task<Book?> DeleteAsync(int id);
        Task<bool> BookExist(int id);
        Task<bool> BookExistByISBN (string isbn);
     
    }
}