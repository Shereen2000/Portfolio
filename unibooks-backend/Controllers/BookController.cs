using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using unibooks_backend.Data;
using unibooks_backend.Dtos.Book;
using unibooks_backend.Interfaces;
using unibooks_backend.Mappers;
using unibooks_backend.Models;
using unibooks_backend.Repositories;

namespace unibooks_backend.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
       private readonly IBookRepository _bookrepo;
     

         public BookController(IBookRepository bookRepo)
         {
            _bookrepo = bookRepo;
           
         }

         
         [HttpPost]
         public async Task<IActionResult> CreateBook(string isbn, string title, string publisher, IFormFile CoverPage)
         {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool bookExist = await _bookrepo.BookExistByISBN(isbn);

            if(bookExist)
            {
                return BadRequest($"Book with ISBN:{isbn} already exists");
            }

            Book book = new Book{
                Title = title,
                Publisher = publisher,
                ISBN = isbn,
            };

            var output = await _bookrepo.CreateAsync(book, CoverPage);

            return Ok(output);
         }

         [HttpGet("{id:int}")]
         public async Task<IActionResult> GetBookById(int id)
         {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookrepo.GetByIdAsync(id);

            if(book == null)
            {
                return NotFound();
            }

            return Ok(book);
         }


         [HttpGet("{isbn}")]
         public async Task<IActionResult> GetBookByIsbn(string isbn)
         {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _bookrepo.GetByISBNAsync(isbn);

            if(book == null)
            {
                return NotFound();
            }

            return Ok(book);
         }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] BookQueryObject query)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var books = await _bookrepo.GetAllAsync(query);

            return Ok(books);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookmodel = await _bookrepo.DeleteAsync(id);


            if(bookmodel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}