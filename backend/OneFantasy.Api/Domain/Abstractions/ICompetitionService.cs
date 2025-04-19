using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface ICompetitionService
    {

        Task<Competition> CreateAsync(CreateCompetitionDto dto);
        Task<Competition> GetByIdAsync(int id);

    }
}
