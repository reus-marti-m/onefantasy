using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ISeasonService
    {
        Task<SeasonDtoResponse> CreateAsync(int competitionId, SeasonDto dto);
        Task<SeasonDtoResponse> UpdateAsync(int seasonId, SeasonDto dto);
        Task<IEnumerable<SeasonDtoResponse>> GetByCompetitionAsync(int competitionId);
        Task<SeasonDtoResponse> GetByIdAsync(int seasonId);
    }
}
