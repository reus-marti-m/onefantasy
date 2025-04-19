using System.ComponentModel.DataAnnotations;
using static OneFantasy.Api.Models.Competitions.Competition;

namespace OneFantasy.Api.DTOs
{
    public class CreateCompetitionDto
    {

        [Required]
        public string Name { get; set; }

        [EnumDataType(typeof(CompetitionType))]
        public CompetitionType Type { get; set; }

        [EnumDataType(typeof(CompetitionFormat))]
        public CompetitionFormat Format { get; set; }
    }
}
