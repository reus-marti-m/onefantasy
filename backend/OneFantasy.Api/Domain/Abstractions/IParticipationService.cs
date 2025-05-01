using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface IParticipationService
    {
        Task<ParticipationStandartDtoResponse> CreateStandardAsync(int seasonId, ParticipationStandartDto dto);
        Task<ParticipationSpecialDtoResponse> CreateSpecialAsync(int seasonId, ParticipationSpecialDto dto);
        Task<ParticipationExtraDtoResponse> CreateExtraAsync(int seasonId, ParticipationExtraDto dto);
        Task<IEnumerable<IParticipationDtoResponse>> GetBySeasonAsync(int seasonId);
        Task<IParticipationDtoResponse> GetByIdAsync(int seasonId, int participationId);
        Task<IEnumerable<IMinigameDtoResponse>> ResolveMinigamesAsync(int seasonId, int participationId, List<ParticipationResultDto> dtos);
    }
}
