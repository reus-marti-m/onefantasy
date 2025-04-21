using System.Linq;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public static class CompetitionMapper
    {
        public static Competition ToCompetition(this CompetitionDto dto) => new(dto.Name, dto.Type, dto.Format);

        public static CompetitionDtoResponse ToDtoResponse(this Competition c) => new()
        {
            Name = c.Name,
            Type = c.Type,
            Format = c.Format,
            Id = c.Id,
            Seasons = c.Seasons.Select(s => s.ToDtoResponse())
        };
    }
}
