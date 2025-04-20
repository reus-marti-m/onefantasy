using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneFantasy.Api.DTOs
{
    public class TeamWithPlayersDto
    {
        [Required]
        public TeamDto Team { get; set; }

        public IEnumerable<PlayerDto> Players { get; set; }
    }
}
