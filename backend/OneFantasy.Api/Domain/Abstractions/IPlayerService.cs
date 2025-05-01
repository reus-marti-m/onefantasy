using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface IPlayerService
    {
        Task<PlayerDtoResponse> CreateAsync(int teamId, PlayerDto dto);
        Task<PlayerDtoResponse> UpdateAsync(int playerId, PlayerDto dto);
        Task<IEnumerable<PlayerDtoResponse>> GetByTeamAsync(int teamId);
        Task<PlayerDtoResponse> GetByIdAsync(int playerId);
    }
}
