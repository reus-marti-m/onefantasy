using System.Linq;
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
            // Això evita l’error: per defecte AutoMapper NO maparà src.Players
            .ForMember(dest => dest.Players, opt => opt.Ignore())
            .AfterMap((dto, team, ctx) =>
            {
                if (dto.Players != null)
                {
                    foreach (var pDto in dto.Players)
                    {
                        // primer posem el team al context
                        ctx.Items["team"] = team;
                        // ara sí que podem Mapar el PlayerDto amb el PlayerProfile
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
