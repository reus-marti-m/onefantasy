using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.DTOs
{
    public class TeamDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(5)]
        public string Abbreviation { get; set; }

        public IEnumerable<PlayerDto> Players { get; set; } = [];
    }

    public class TeamDtoResponse : TeamDto
    {
        public int Id { get; set; }
    }
}
