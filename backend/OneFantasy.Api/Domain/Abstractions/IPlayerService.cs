using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface IPlayerService
    {
        Task<Player> CreateAsync(int teamId, PlayerDto dto);
        Task<Player> UpdateAsync(int playerId, PlayerDto dto);
        Task<IEnumerable<Player>> GetByTeamAsync(int teamId);
        Task<Player> GetByIdAsync(int playerId);
    }
}
