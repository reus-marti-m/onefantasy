using AutoMapper;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamDto, Team>()
                .ConstructUsing((src, ctx) => new Team(src.Name, src.Abbreviation, (Season)ctx.Items["season"]));
            CreateMap<Team, TeamDtoResponse>()
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players));
        }
    }
}
