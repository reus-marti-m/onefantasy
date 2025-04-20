using System.ComponentModel.DataAnnotations;

namespace OneFantasy.Api.DTOs
{
    public class TeamDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(5)]
        public string Abbreviation { get; set; }
    }
}
