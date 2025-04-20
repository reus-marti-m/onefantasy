using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneFantasy.Api.Models.Authentication;
using OneFantasy.Api.DTOs;
using System.Linq;
using OneFantasy.Api.Domain.Abstractions;

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
        public async Task<IActionResult> Guest() => Ok(new { token = await _auth.GuestAsync() });

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AuthDto dto) => Ok(new { token = await _auth.RegisterAsync(dto) });

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthDto dto) => Ok(new { token = await _auth.LoginAsync(dto) });

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
