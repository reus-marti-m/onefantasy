using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ICompetitionService
    {
        Task<CompetitionDtoResponse> CreateAsync(CompetitionDto dto);
        Task<CompetitionDtoResponse> UpdateAsync(int id, CompetitionDto dto);
        Task<CompetitionDtoResponse> GetByIdAsync(int id);
        Task<IEnumerable<CompetitionDtoResponse>> GetAllAsync();
    }
}
