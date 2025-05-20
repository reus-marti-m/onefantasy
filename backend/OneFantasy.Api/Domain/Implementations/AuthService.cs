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
using System.Security.Cryptography;
using OneFantasy.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace OneFantasy.Api.Domain.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _users;
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly IConfiguration _config;
        private readonly AppDbContext _db;

        public AuthService(
            UserManager<ApplicationUser> users,
            SignInManager<ApplicationUser> signIn,
            IConfiguration config,
            AppDbContext db)
        {
            _users = users;
            _signIn = signIn;
            _config = config;
            _db = db;
        }

        public LoginResponseDto Guest()
        {
            var guestId = Guid.NewGuid().ToString();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, guestId),
                new Claim(ClaimTypes.Role, "Guest")
            };

            var accessToken = GenerateJwt(claims);

            return new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = null
            };
        }

        public async Task<LoginResponseDto> RegisterAsync(AuthDto dto)
        {
            if (await _users.FindByEmailAsync(dto.Email) != null)
                throw new DuplicateUserException(dto.Email);

            var user = new ApplicationUser(dto.Email);
            var res = await _users.CreateAsync(user, dto.Password);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));

            await _users.AddToRoleAsync(user, "User");
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            };
            var accessToken = GenerateJwt(claims);
            var refreshToken = await CreateRefreshToken(user.Id);

            return new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<LoginResponseDto> LoginAsync(AuthDto dto)
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

            var accessToken = GenerateJwt(claims);
            var refreshToken = await CreateRefreshToken(user.Id);

            return new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task AdminRegisterAsync(AuthDto dto, string currentUserId)
        {
            var current = await _users.FindByIdAsync(currentUserId)
                        ?? throw new InvalidCredentialsException();
            if (!await _users.IsInRoleAsync(current, "Admin"))
                throw new UnauthorizedAdminException();

            if (await _users.FindByEmailAsync(dto.Email) != null)
                throw new DuplicateUserException(dto.Email);

            var admin = new ApplicationUser(dto.Email);
            var res = await _users.CreateAsync(admin, dto.Password);
            if (!res.Succeeded)
                throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));

            await _users.AddToRoleAsync(admin, "Admin");
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
            var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

            var saved = await _db.RefreshTokens
                                 .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

            if (saved == null || saved.UserId != userId)
                throw new SecurityTokenException("Invalid refresh token");

            if (saved.IsExpired)
            {
                _db.RefreshTokens.Remove(saved);
                await _db.SaveChangesAsync();
                throw new SecurityTokenException("Refresh token has expired");
            }

            var newAccess = GenerateJwt(principal.Claims);
            saved.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            // Expires no canvia quan usuari fa login.
            // Si es vol fer que la sessio duri més si l'usuari és actiu, implementar un altre camp a BD i combinar-ho amb l'expires
            //var newExpires = DateTime.UtcNow.AddDays(
            //                    int.Parse(_config["Jwt:RefreshTokenExpireDays"] ?? "7"));

            //saved.Expires = newExpires;

            _db.RefreshTokens.Update(saved);
            await _db.SaveChangesAsync();

            return new LoginResponseDto
            {
                Token = newAccess,
                RefreshToken = saved.Token
            };
        }

        public Task<RefreshToken> GetByUserId(string userId) =>
            _db.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(rt => rt.UserId == userId);


        #region Helpers

        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(double.Parse(jwt["ExpireMinutes"]));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.InboundClaimTypeMap.Clear();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwt ||
                !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        private async Task<RefreshToken> CreateRefreshToken(string userId)
        {
            var jwtSection = _config.GetSection("Jwt");
            int days = int.Parse(jwtSection["RefreshTokenExpireDays"] ?? "7");

            var token = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(token);

            var refresh = new RefreshToken
            {
                Token = Convert.ToBase64String(token),
                Expires = DateTime.UtcNow.AddDays(days),
                UserId = userId
            };

            _db.RefreshTokens.Add(refresh);
            await _db.SaveChangesAsync();
            return refresh;
        }

        #endregion


    }
}
