using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Authentication;
using OneFantasy.Api.Domain.Exceptions;
using System.Linq;

namespace OneFantasy.Api.Domain.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _users;
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly IConfiguration _config;

        public AuthService(
            UserManager<ApplicationUser> users,
            SignInManager<ApplicationUser> signIn,
            IConfiguration config)
        {
            _users = users;
            _signIn = signIn;
            _config = config;
        }

        public Task<string> GuestAsync()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Guest")
            };
            return Task.FromResult(GenerateJwt(claims));
        }

        public async Task<string> RegisterAsync(AuthDto dto)
        {
            if (await _users.FindByEmailAsync(dto.Email) != null)
                throw new DuplicateUserException(dto.Email);

            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var res = await _users.CreateAsync(user, dto.Password);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));

            await _users.AddToRoleAsync(user, "User");
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            };
            return GenerateJwt(claims);
        }

        public async Task<string> LoginAsync(AuthDto dto)
        {
            var user = await _users.FindByEmailAsync(dto.Email)
                       ?? throw new InvalidCredentialsException();

            var signRes = await _signIn.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!signRes.Succeeded)
                throw new InvalidCredentialsException();

            var roles = await _users.GetRolesAsync(user);
            var claims = new List<Claim> {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(ClaimTypes.Name, user.UserName)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            return GenerateJwt(claims);
        }

        public async Task AdminRegisterAsync(AuthDto dto, string currentUserId)
        {
            var current = await _users.FindByIdAsync(currentUserId)
                        ?? throw new InvalidCredentialsException();
            if (!await _users.IsInRoleAsync(current, "Admin"))
                throw new UnauthorizedAdminException();

            if (await _users.FindByEmailAsync(dto.Email) != null)
                throw new DuplicateUserException(dto.Email);

            var admin = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var res = await _users.CreateAsync(admin, dto.Password);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));

            await _users.AddToRoleAsync(admin, "Admin");
        }

        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpireMinutes"]));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
