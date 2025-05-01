using AutoMapper;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public class SeasonProfile : Profile
    {
        public SeasonProfile()
        {
            CreateMap<SeasonDto, Season>()
                .ConstructUsing((src, ctx) => new Season(src.Year, (Competition)ctx.Items["competition"]));
            CreateMap<Season, SeasonDtoResponse>();
        }
    }
}
