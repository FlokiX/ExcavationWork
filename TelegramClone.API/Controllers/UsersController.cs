﻿using Microsoft.AspNetCore.Mvc;
using TelegramClone.API.Contracts;
using TelegramClone.Core.Abstractions;
using TelegramClone.Core.Models;


namespace TelegramClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }




        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRequest request)
        {

            var (user, error) = await _userService.RegisterAsync(request.Username, request.Email, request.Password);

            if (error != null) return BadRequest(error);

            
            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var (userId, error) = await _userService.LoginAsync(request.Email, request.Password);

            if (error != null)
            {
                return BadRequest(error);
            }

            
            return Ok(new { UserId = userId });
        }


        /*// PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            
            var existingUser = await _userService.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

           
            var (updatedUser, error) = TelegramClone.Core.Models.User.Create(id, request.Username, request.Email, request.PasswordHash);

            if (error != null)
            {
                return BadRequest(error);
            }

            х
            await _userService.UpdateAsync(updatedUser.Id, updatedUser.Username, updatedUser.Email, updatedUser.PasswordHash);

            return Ok(updatedUser); 
        }


        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }*/




        /* // GET: api/users
         [HttpGet]
         public async Task<ActionResult<IEnumerable<User>>> GetUsers()
         {
             var users = await _userService.GetAllAsync();
             return Ok(users);
         }

         // GET: api/users/{id}
         [HttpGet("{id}")]
         public async Task<ActionResult<User>> GetUser(Guid id)
         {
             var user = await _userService.GetByIdAsync(id);
             if (user == null)
             {
                 return NotFound();
             }
             return Ok(user);
         }*/
    }
}
