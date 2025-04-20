using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ISeasonService
    {
        Task<CompetitionSeason> CreateAsync(int competitionId, SeasonDto dto);
        Task<CompetitionSeason> UpdateAsync(int seasonId, SeasonDto dto);
        Task<IEnumerable<CompetitionSeason>> GetByCompetitionAsync(int competitionId);
        Task<CompetitionSeason> GetByIdAsync(int seasonId);
    }
}
