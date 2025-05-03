using AutoMapper;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDto, Player>()
                .ConstructUsing((src, ctx) => new Player(src.Name, (Team)ctx.Items["team"]));
            CreateMap<PlayerDtoResponse, Player>()
                .ConstructUsing((src, ctx) => new Player(src.Name, (Team)ctx.Items["team"]) { Id = src.Id });
            CreateMap<Player, PlayerDtoResponse>();
        }
    }
}
