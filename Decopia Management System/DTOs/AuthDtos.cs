using System.ComponentModel.DataAnnotations;

namespace Decopia.API.DTOs
{

    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

     public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }

     public class ForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

     public class ResetPasswordRequestDto
    {

        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }

    }
}
