using System.Collections.Generic;
using static OneFantasy.Api.Models.Competitions.Competition;
using System.ComponentModel.DataAnnotations;
using OneFantasy.Api.Models.Competitions;
using System.Linq;

namespace OneFantasy.Api.DTOs
{
    public class CompetitionDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [EnumDataType(typeof(CompetitionType))]
        public CompetitionType Type { get; set; }

        [EnumDataType(typeof(CompetitionFormat))]
        public CompetitionFormat Format { get; set; }
    }

    public class CompetitionDtoResponse : CompetitionDto
    {
        public int Id { get; set; }
        public IEnumerable<SeasonDtoResponse> Seasons { get; set; } = [];
    }

    public static class CompetitionDtoExtensions
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
