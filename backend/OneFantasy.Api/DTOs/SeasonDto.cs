using System.ComponentModel.DataAnnotations;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.DTOs
{
    public class SeasonDto
    {
        [Required]
        public int Year { get; set; }
    }

    public class SeasonDtoResponse : SeasonDto
    {
        public int Id { get; set; }
    }

    public static class SeasonDtoExtensions
    {
        public static Season ToSeason(this SeasonDto s, Competition c) => new(s.Year, c);

        public static SeasonDtoResponse ToDtoResponse(this Season s) => new()
        {
            Year = s.Year,
            Id = s.Id
        };
    }
}
