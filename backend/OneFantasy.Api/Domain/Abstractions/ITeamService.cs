using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ITeamService
    {
        Task<Team> CreateAsync(int seasonId, TeamDto dto);
        Task<Team> UpdateAsync(int teamId, TeamDto dto);
        Task<IEnumerable<Team>> GetBySeasonAsync(int seasonId);
        Task<Team> GetByIdAsync(int teamId);
    }
}
