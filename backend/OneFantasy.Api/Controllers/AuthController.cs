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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("guest")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public IActionResult Guest() => Ok(_auth.Guest());

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Register([FromBody] AuthDto dto) => Ok(await _auth.RegisterAsync(dto));

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Produces("application/json", "application/problem+json")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto) => Ok(await _auth.LoginAsync(dto));

        [HttpPost("admin/register")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> AdminRegister([FromBody] AuthDto dto)
        {
            var currentUserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            await _auth.AdminRegisterAsync(dto, currentUserId);
            return Ok();
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto dto)
        {
            var response = await _auth.RefreshTokenAsync(dto);
            return Ok(response);
        }
    }

}
