using OneFantasy.Api.DTOs;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface IAuthService
    {
        Task<string> GuestAsync();
        Task<string> RegisterAsync(AuthDto dto);
        Task<string> LoginAsync(AuthDto dto);
        Task AdminRegisterAsync(AuthDto dto, string currentUserId);
    }
}
