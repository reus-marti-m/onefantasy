using OneFantasy.Api.DTOs;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Abstractions
{
    public interface IParticipationService
    {
        Task<ParticipationStandartDtoResponse> CreateStandardAsync(int seasonId, ParticipationStandartDto dto);
        Task<ParticipationSpecialDtoResponse> CreateSpecialAsync(int seasonId, ParticipationSpecialDto dto);
        Task<ParticipationExtraDtoResponse> CreateExtraAsync(int seasonId, ParticipationExtraDto dto);
    }
}
