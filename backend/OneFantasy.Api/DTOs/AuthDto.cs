using System.ComponentModel.DataAnnotations;

namespace OneFantasy.Api.DTOs
{
    public class AuthDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
    }
}
