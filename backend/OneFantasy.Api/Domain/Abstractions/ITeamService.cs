using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ITeamService
    {
        Task<TeamDtoResponse> CreateAsync(int seasonId, TeamDto dto);
        Task<TeamDtoResponse> UpdateAsync(int teamId, TeamDto dto);
        Task<IEnumerable<TeamDtoResponse>> GetBySeasonAsync(int seasonId);
        Task<TeamDtoResponse> GetByIdAsync(int teamId);
    }
}
