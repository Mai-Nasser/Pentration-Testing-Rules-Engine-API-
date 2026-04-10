using Decopia.API.DTOs;
using Decopia.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Decopia.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

         [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Invalid email or password");

            return Ok(result);
        }

         [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto dto)
        {
            var success = await _authService.ForgotPasswordAsync(dto);
            if (!success)
                return NotFound("User with this email not found");

            return Ok("Reset code sent.");
        }

         [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto dto)
        {
            var success = await _authService.ResetPasswordAsync(dto);
            if (!success)
                return BadRequest("Invalid reset code or email");

            return Ok("Password reset successfully.");
        }
    }
}
