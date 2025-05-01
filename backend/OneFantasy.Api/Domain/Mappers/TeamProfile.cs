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
            .ConstructUsing((src, ctx) =>
                new Team(src.Name, src.Abbreviation, (Season)ctx.Items["season"]))
            .ForMember(dest => dest.Players, opt => opt.Ignore())
            .AfterMap((dto, team, ctx) =>
            {
                if (dto.Players != null)
                {
                    team.Players = [];
                    foreach (var pDto in dto.Players)
                    {
                        ctx.Items["team"] = team;
                        var player = ctx.Mapper.Map<Player>(pDto);
                        team.Players.Add(player);
                    }
                }
            });

            CreateMap<Team, TeamDtoResponse>()
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players));
        }
    }
}
