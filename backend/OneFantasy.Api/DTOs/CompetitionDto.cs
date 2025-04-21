using System.Collections.Generic;
using static OneFantasy.Api.Models.Competitions.Competition;
using System.ComponentModel.DataAnnotations;

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

}
