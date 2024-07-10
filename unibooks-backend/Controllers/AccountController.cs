using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using unibooks_backend.Models;
using unibooks_backend.Interfaces;
using unibooks_backend.Dtos.Account;
using unibooks_backend.Dtos;

namespace unibooks_backend.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<APPUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<APPUser> _signInManager;

        public AccountController(UserManager<APPUser> userManager, ITokenService tokenService, SignInManager<APPUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

         [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
                try {
                    if(!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var appUser = new APPUser
                    {
                        UserName = registerDto.Username,
                        Email = registerDto.Email //you can just do the email if you want to
                    };

                    var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                    if(createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                        if(roleResult.Succeeded)
                        {
                           // return Ok("User created");
                            return Ok(
                                        new NewUserDto
                                        {
                                            UserName = appUser.UserName,
                                            Email = appUser.Email,
                                            Token = _tokenService.CreateToken(appUser),
                                        }
                                    );
                        }else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                    }else
                    {
                        return StatusCode(500, createdUser.Errors);
                    }

                } catch (Exception e)
                {
                    return StatusCode(500,e); //run migration to see roles dotnet ef migrations add seedRole
                }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());


            if(user == null){return Unauthorized("Invalid username");}

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

            if(!result.Succeeded) {return Unauthorized("UserName not found and/or password incorrect");}

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

    }
}