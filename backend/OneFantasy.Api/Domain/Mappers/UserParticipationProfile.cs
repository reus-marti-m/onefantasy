using System;
using System.Linq;
using AutoMapper;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Authentication;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.Users;

namespace OneFantasy.Api.Domain.Mappers
{
    public class UserParticipationProfile : Profile
    {

        public UserParticipationProfile()
        {
            CreateMap<int, UserOption>()
                .ConstructUsing(id => new UserOption(id))
                .ForMember(dest => dest.UserMinigameId, opt => opt.Ignore())
                .ForMember(dest => dest.UserMinigame, opt => opt.Ignore());

            CreateMap<UserPlayMinigameDto, UserMinigame>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserMinigameGroupId, opt => opt.Ignore())
                .ForMember(dest => dest.UserMinigameGroup, opt => opt.Ignore())
                .ForMember(dest => dest.MinigameId, opt => opt.MapFrom(src => src.MinigameId))
                .ForMember(dest => dest.Minigame, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.UserOptions, opt => opt.MapFrom(src => src.SelectedOptionIds));

            CreateMap<UserMinigame, UserMinigameResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MinigameId, opt => opt.MapFrom(src => src.MinigameId))
                .ForMember(dest => dest.SelectedOptionIds, opt => opt.MapFrom(src => src.UserOptions.Select(uo => uo.OptionId).ToList()));

            CreateMap<UserPlayGroupDto, UserMinigameGroup>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserParticipationId, opt => opt.Ignore())
                .ForMember(dest => dest.UserParticipation, opt => opt.Ignore())
                .ForMember(dest => dest.MinigameGroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.MinigameGroup, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.UserMinigames, opt => opt.MapFrom(src => src.Minigames));

            CreateMap<UserMinigameGroup, UserParticipationGroupResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.MinigameGroupId))
                .ForMember(dest => dest.Minigames, opt => opt.MapFrom(src => src.UserMinigames));

            CreateMap<CreateUserParticipationDto, UserParticipation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ParticipationId, opt => opt.Ignore())
                .ForMember(dest => dest.Participation, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdate, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups))
                .AfterMap((src, dest, ctx) =>
                {
                    var user = ctx.Items["user"] as ApplicationUser;
                    dest.User = user;
                    dest.UserId = user.Id;

                    var part = ctx.Items["participation"] as Participation;
                    dest.Participation = part;
                    dest.ParticipationId = part.Id;

                    dest.LastUpdate = DateTime.Now;
                    dest.UsedBudget = (int)ctx.Items["usedBudget"];
                });

            CreateMap<UserParticipation, UserParticipationResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate))
                .ForMember(dest => dest.UsedBudget, opt => opt.MapFrom(src => src.UsedBudget))
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));
        }

    }
}
