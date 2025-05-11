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
        Task<List<IParticipationDtoResponse>> GetBySeasonAsync(int seasonId, string userId, bool isAdmin);
        Task<IParticipationDtoResponse> GetByIdAsync(int seasonId, int participationId, string userId, bool isAdmin);
        Task<List<IMinigameDtoResponse>> ResolveMinigamesAsync(int seasonId, int participationId, List<ParticipationResultDto> dtos);
        Task<UserParticipationResponseDto> CreateUserParticipationAsync(int seasonId, int participationId, string userId, CreateUserParticipationDto dto);
    }
}
