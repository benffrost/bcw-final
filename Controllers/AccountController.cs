using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserRepository _repo;
        public AccountController(UserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegistration creds)
        {
            try
            {
                User user = _repo.Register(creds);
                if (user == null) { Unauthorized("Invalid Credentials"); }
                user.SetClaims();
                await HttpContext.SignInAsync(user._principal);
                return Ok(user);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLogin creds)
        {
            try
            {
                User user = _repo.Login(creds);
                if (user == null) { Unauthorized("Invalid Credentials"); }
                user.SetClaims();
                await HttpContext.SignInAsync(user._principal);
                return Ok(user);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("Logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return Ok(true);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [Authorize] // Only Authenticated users will be allowed into this method
        [HttpGet("Authenticate")]
        public async Task<ActionResult<User>> Authenticate()
        {
            try
            {
                var id = HttpContext.User.FindFirstValue("Id"); // THIS IS HOW YOU GET THE ID of the currently logged in user
                var user = _repo.GetUserById(id);
                if (user == null) { 
                    await HttpContext.SignOutAsync();
                    throw new Exception("User not logged In"); 
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}