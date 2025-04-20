using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ICompetitionService
    {
        Task<Competition> CreateAsync(CompetitionDto dto);
        Task<Competition> UpdateAsync(int id, CompetitionDto dto);
        Task<Competition> GetByIdAsync(int id);
        Task<IEnumerable<Competition>> GetAllAsync();
    }
}
