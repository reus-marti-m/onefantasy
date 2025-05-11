using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("guest")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Guest() => Ok(new LoginResponseDto { Token = await _auth.GuestAsync() });

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] AuthDto dto) => Ok(new LoginResponseDto { Token = await _auth.RegisterAsync(dto) });

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto) => Ok(new LoginResponseDto { Token = await _auth.LoginAsync(dto) });

        [HttpPost("admin/register")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> AdminRegister([FromBody] AuthDto dto)
        {
            var currentUserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            await _auth.AdminRegisterAsync(dto, currentUserId);
            return Ok();
        }
    }

}
