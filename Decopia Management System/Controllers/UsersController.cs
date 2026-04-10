using Decopia.API.DTOs;
using Decopia.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Decopia.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

         [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            await _userService.CreateUserAsync(dto);
            return Ok(new { message = "User created successfully" });
        }

         [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

         [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string name)
        {
            var users = await _userService.SearchUsersByNameAsync(name);
            return Ok(users);
        }

         [HttpGet("{publicId}")]
        public async Task<IActionResult> GetUser(Guid publicId)
        {
            var user = await _userService.GetUserAsync(publicId);
            return Ok(user);
        }

         [HttpPut("{publicId}")]
        public async Task<IActionResult> UpdateUser(Guid publicId, [FromBody] UpdateUserDto dto)
        {
            await _userService.UpdateUserAsync(publicId, dto);
            return Ok(new { message = "User updated successfully" });
        }

         [HttpPatch("{publicId}/activate")]
        public async Task<IActionResult> ChangeStatus(Guid publicId, [FromQuery] bool isActive)
        {
            await _userService.ChangeStatusAsync(publicId, isActive);
            return Ok(new { message = "Status updated successfully" });
        }

         [HttpDelete("{publicId}")]
        public async Task<IActionResult> DeleteUser(Guid publicId)
        {
            var success = await _userService.DeleteUserAsync(publicId);
            if (!success)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
