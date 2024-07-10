using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using unibooks_backend.Extensions;
using unibooks_backend.Interfaces;
using unibooks_backend.Models;

namespace unibooks_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookMarksController : ControllerBase
    {
        private readonly UserManager<APPUser> _userManager;
        private readonly IAdvertRepository _advertRepo;
        private readonly IBookMarkRepository _bookMarkRepo;

      public BookMarksController(UserManager<APPUser> userManager, IAdvertRepository advertRepo, IBookMarkRepository bookMarkRepo)
      {
        _userManager = userManager;
        _advertRepo = advertRepo;
        _bookMarkRepo = bookMarkRepo;   
      }

       [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);
            var UserBookmarks= await _bookMarkRepo.GetUserBookmarksAsync(AppUser);
            return Ok(UserBookmarks);
        }


        [HttpPost]
        public async Task<IActionResult> AddBookmark(int advertId)
        {
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);

            var Advert = await _advertRepo.GetByIdAsync(advertId);

                  if (Advert == null)
                {
                    

                    return BadRequest("Advert you want to bookmark does not found");
                }

            

            var UserBookmarks = await _bookMarkRepo.GetUserBookmarksAsync(AppUser);

            if(UserBookmarks.Any(e => e.Id == advertId)) return BadRequest("Advert already bookmarked");

            var BookmarkModel = new BookMark
            {
                AdvertId =  Advert.Id,
                AppUserId = AppUser.Id
            };

            var ReturnedBookmarkModel = await _bookMarkRepo.CreateAsync(BookmarkModel);

            if( ReturnedBookmarkModel == null)
            {
                return StatusCode(500, "could not create");
            }
            else{
                return Created();
            }
        }
    }
}
         
    
