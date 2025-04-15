using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MarkDownNoteApi.Dtos.AccountDtos;
using MarkDownNoteApi.Interface;
using MarkDownNoteApi.Models;
using MarkDownNoteApi.Servce;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarkDownNoteApi.Controller
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager ; 
        private readonly ITokenService _tokenService ;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
           this._userManager= userManager;
           this._tokenService=tokenService;
            this._signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> register ([FromBody]RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try{
                var user = new AppUser
                {
                    UserName= registerDto.UserName,
                    Email= registerDto.Email

                };
                var createUser = await _userManager.CreateAsync(user, registerDto.Password);
                if (createUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, "User");
                    if (addRole.Succeeded)
                    {
                        return Ok(new userDto
                        {
                            UserName= user.UserName,
                            Email= user.Email,
                            Token= _tokenService.CreateToken(user)

                        });
                    }
                    else
                    {
                        return StatusCode(500, addRole.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login ([FromBody] LoginDto loginDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = await _userManager.Users.FirstOrDefaultAsync(email=> email.Email == loginDto.Email);
            if (appUser == null)
            {
                return Unauthorized("Email or/and password wrong");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Email or/and password wrong");
            }
            return Ok(new userDto
            {
                UserName= appUser.UserName,
                Email= appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            });
        }


    }
}