using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ICompetitionService
    {
        Task<CompetitionDtoResponse> CreateAsync(CompetitionDto dto);
        Task<CompetitionDtoResponse> UpdateAsync(int id, CompetitionDto dto);
        Task<CompetitionDtoResponse> GetByIdAsync(int id);
        Task<List<CompetitionDtoResponse>> GetAllAsync();
    }
}
