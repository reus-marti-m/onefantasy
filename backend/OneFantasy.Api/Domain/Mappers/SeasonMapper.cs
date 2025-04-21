using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public static class SeasonMapper
    {

        public static Season ToSeason(this SeasonDto s, Competition c) => new(s.Year, c);

        public static SeasonDtoResponse ToDtoResponse(this Season s) => new()
        {
            Year = s.Year,
            Id = s.Id
        };

    }
}
