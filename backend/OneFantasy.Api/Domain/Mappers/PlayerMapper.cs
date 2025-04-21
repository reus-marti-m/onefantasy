using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public static class PlayerMapper
    {

        public static Player ToPlayer(this PlayerDto p, Team team) => new(p.Name, team);

        public static PlayerDtoResponse ToDtoResponse(this Player p) => new()
        {
            Name = p.Name,
            Id = p.Id
        };

    }
}
