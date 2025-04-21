using System.ComponentModel.DataAnnotations;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.DTOs
{
    public class PlayerDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
    }

    public class PlayerDtoResponse : PlayerDto
    {
        public int Id { get; set; }
    }
}
