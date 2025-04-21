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

    public static class PlayerDtoExtensions
    {
        public static Player ToPlayer(this PlayerDto p, Team team) => new(p.Name, team);

        public static PlayerDtoResponse ToDtoResponse(this Player p) => new()
        {
            Name = p.Name,
            Id = p.Id
        };
    }
}
