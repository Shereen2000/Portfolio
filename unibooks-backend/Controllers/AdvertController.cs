using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Util.Internal;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using unibooks_backend.Dtos.Advert;
using unibooks_backend.Dtos.Book;
using unibooks_backend.Interfaces;
using unibooks_backend.Mappers;
using unibooks_backend.Models;
using unibooks_backend.Repositories;

namespace unibooks_backend.Controllers
{
    [ApiController]
    [Route("api/advert")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertRepository _advertRepo;
        private readonly IBookRepository _bookRepo;

        public AdvertController(IAdvertRepository advertRepo, IBookRepository bookRepo)
        {
            _advertRepo = advertRepo;
            _bookRepo = bookRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvert(int bookId, decimal price, string condition, List<IFormFile> bookPics)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool exist = await _bookRepo.BookExist(bookId);

            if(!exist)
            {
                return BadRequest("The book you want to create an Advert for does not exist");
            }

            var book = await _bookRepo.GetByIdAsync(bookId);

            Advert advert = new Advert{
                Price = price,
                BookId = bookId,
                Condition = condition,
                Book = book
            };

            var output = await _advertRepo.CreateAsync(advert, bookPics);

            return Ok(output);
        }

         [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] AdvertQueryObject query)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adverts = await _advertRepo.GetAllAsync(query);

            return Ok(adverts.Select(a=>a.ToAdvertDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAdvert(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advert = await _advertRepo.GetByIdAsync(id);

            if(advert == null)
            {
                return NotFound();
            }

            return Ok(advert.ToAdvertDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertmodel = await _advertRepo.DeleteAsync(id);

            if(advertmodel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}