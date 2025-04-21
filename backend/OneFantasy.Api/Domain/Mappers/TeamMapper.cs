using System.Linq;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public static class TeamMapper
    {

        public static Team ToTeam(this TeamDto t, Season season) => new(t.Name, t.Abbreviation, season);

        public static TeamDtoResponse ToDtoResponse(this Team t) => new()
        {
            Name = t.Name,
            Abbreviation = t.Abbreviation,
            Id = t.Id,
            Players = t.Players.Select(p => p.ToDtoResponse())
        };

    }
}
