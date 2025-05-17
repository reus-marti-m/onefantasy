using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Authentication;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface IAuthService
    {
        LoginResponseDto Guest();
        Task<LoginResponseDto> RegisterAsync(AuthDto dto);
        Task<LoginResponseDto> LoginAsync(AuthDto dto);
        Task AdminRegisterAsync(AuthDto dto, string currentUserId);
        Task<RefreshToken> GetByUserId(string userId);
        Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto dto);
    }
}
