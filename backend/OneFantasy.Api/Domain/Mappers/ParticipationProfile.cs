using System.Linq;
using AutoMapper;
using OneFantasy.Api.Domain.Helpers;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;
using OneFantasy.Api.Models.Participations.Minigames;
using OneFantasy.Api.Models.Participations.Users;

namespace OneFantasy.Api.Domain.Mappers
{
    public class ParticipationProfile : Profile
    {
        public ParticipationProfile()
        {
            // --- Options ---
            CreateMap<OptionDto, Option>()
                .ConstructUsing(dto => new Option(ProbabilityPriceCalculator.GetPrice(dto.Probability)));
            CreateMap<Option, OptionDtoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.HasOccurred, opt => opt.MapFrom(src => src.HasOccurred))
                .ForMember(dest => dest.IsPlayed, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return (bool?)null;
                    var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.Minigame.GroupId);
                    var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == src.Minigame.Id);
                    return userMg != null && userMg.UserOptions.Any(uo => uo.OptionId == src.Id);
                }));

            CreateMap<OptionPlayerDto, OptionPlayer>()
                .ConstructUsing(dto => new OptionPlayer(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.PlayerId
                ));
            CreateMap<OptionPlayer, OptionPlayerDtoResponse>();

            CreateMap<OptionScoreDto, OptionScore>()
                .ConstructUsing(dto => new OptionScore(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.HomeGoals,
                    dto.AwayGoals
                ));
            CreateMap<OptionScore, OptionScoreDtoResponse>();

            CreateMap<OptionTeamDto, OptionTeam>()
                .ConstructUsing(dto => new OptionTeam(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.TeamId
                ));
            CreateMap<OptionTeam, OptionTeamDtoResponse>();

            CreateMap<OptionIntervalDto, OptionInterval>()
                .ConstructUsing(dto => FromDto(dto));
            CreateMap<OptionInterval, OptionIntervalDtoResponse>();

            // --- Minigames ---
            CreateMap<MinigameMatchDto, MinigameMatch>()
                .ConstructUsing(dto => new MinigameMatch(
                    dto.Options.Select(FromDto).ToList(),
                    dto.Type
                ))
                .ForMember(dest => dest.Options, opt => opt.Ignore());
            CreateMap<MinigameMatch, MinigameMatchDtoResponse>()
                .IncludeBase<Minigame, IMinigameDtoResponse>()
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return null;
                    var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.GroupId);
                    var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == src.Id);
                    return userMg?.Points;
                }));

            CreateMap<MinigamePlayersDto, MinigamePlayers>()
                .ConstructUsing(dto => new MinigamePlayers(
                    dto.Options.Select(o => new OptionPlayer(
                        ProbabilityPriceCalculator.GetPrice(o.Probability),
                        o.PlayerId
                    )).ToList(),
                    dto.Type
                ))
                .ForMember(dest => dest.Options, opt => opt.Ignore());
            CreateMap<MinigamePlayers, MinigamePlayersDtoResponse>()
                .IncludeBase<Minigame, IMinigameDtoResponse>()
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return (int?)null;
                    var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.GroupId);
                    var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == src.Id);
                    return userMg?.Points;
                }));

            CreateMap<MinigameScoresDto, MinigameScores>()
                .ConstructUsing(dto => new MinigameScores(
                    dto.Options.Select(o => new OptionScore(
                        ProbabilityPriceCalculator.GetPrice(o.Probability),
                        o.HomeGoals,
                        o.AwayGoals
                    )).ToList()
                ))
                .ForMember(dest => dest.Options, opt => opt.Ignore());
            CreateMap<MinigameScores, MinigameScoresDtoResponse>()
                .IncludeBase<Minigame, IMinigameDtoResponse>()
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return null;
                    var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.GroupId);
                    var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == src.Id);
                    return userMg?.Points;
                }));

            CreateMap<MinigameResultDto, MinigameResult>()
                .ForCtorParam("homeVictory", opt => opt.MapFrom(src => src.HomeVictory))
                .ForCtorParam("drawPrice", opt => opt.MapFrom(src => ProbabilityPriceCalculator.GetPrice(src.Draw.Probability)))
                .ForCtorParam("visitingVictory", opt => opt.MapFrom(src => src.VisitingVictory))
                .ForMember(dest => dest.Options, opt => opt.Ignore());
            CreateMap<MinigameResult, MinigameResultDtoResponse>()
                .IncludeBase<Minigame, IMinigameDtoResponse>()
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return null;
                    var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.GroupId);
                    var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == src.Id);
                    return userMg?.Points;
                }));

            //CreateMap<MinigameGroup, IMinigameGroupDtoResponse>()
            //    .Include<MinigameGroupMatch2A, MinigameGroupMatch2ADtoResponse>()
            //    .Include<MinigameGroupMatch2B, MinigameGroupMatch2BDtoResponse>()
            //    .Include<MinigameGroupMatch3, MinigameGroupMatch3DtoResponse>()
            //    .Include<MinigameGroupMulti, MinigameGroupMultiDtoResponse>();

            // --- Minigame Groups 2A / 2B / 3 / Multi ---
            CreateMap<MinigameGroupMatch2ADto, MinigameGroupMatch2A>()
                .ForCtorParam("minigameScores", opt => opt.MapFrom(src => src.MinigameScores))
                .ForCtorParam("minigamePlayers", opt => opt.MapFrom(src => src.MinigamePlayers))
                .ForCtorParam("homeTeamId", opt => opt.MapFrom(src => src.HomeTeamId))
                .ForCtorParam("visitingTeamId", opt => opt.MapFrom(src => src.VisitingTeamId));
            CreateMap<MinigameGroupMatch2A, MinigameGroupMatch2ADtoResponse>()
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return null;
                    var userGrp = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.Id);
                    return userGrp?.Points;
                }));

            CreateMap<MinigameGroupMatch2BDto, MinigameGroupMatch2B>()
                .ForCtorParam("minigameMatch", opt => opt.MapFrom(src => src.MinigameMatch))
                .ForCtorParam("minigamePlayers", opt => opt.MapFrom(src => src.MinigamePlayers))
                .ForCtorParam("homeTeamId", opt => opt.MapFrom(src => src.HomeTeamId))
                .ForCtorParam("visitingTeamId", opt => opt.MapFrom(src => src.VisitingTeamId));
            CreateMap<MinigameGroupMatch2B, MinigameGroupMatch2BDtoResponse>()
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return (int?)null;
                    var userGrp = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.Id);
                    return userGrp?.Points;
                }));

            CreateMap<MinigameGroupMatch3Dto, MinigameGroupMatch3>()
                .ForCtorParam("minigameScores", opt => opt.MapFrom(src => src.MinigameScores))
                .ForCtorParam("minigamePlayers1", opt => opt.MapFrom(src => src.MinigamePlayers1))
                .ForCtorParam("minigamePlayers2", opt => opt.MapFrom(src => src.MinigamePlayers2))
                .ForCtorParam("homeTeamId", opt => opt.MapFrom(src => src.HomeTeamId))
                .ForCtorParam("visitingTeamId", opt => opt.MapFrom(src => src.VisitingTeamId));
            CreateMap<MinigameGroupMatch3, MinigameGroupMatch3DtoResponse>()
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return null;
                    var userGrp = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.Id);
                    return userGrp?.Points;
                }));

            CreateMap<MinigameGroupMultiDto, MinigameGroupMulti>()
                .ForCtorParam("match1", opt => opt.MapFrom(src => src.Match1))
                .ForCtorParam("match2", opt => opt.MapFrom(src => src.Match2))
                .ForCtorParam("match3", opt => opt.MapFrom(src => src.Match3));
            CreateMap<MinigameGroupMulti, MinigameGroupMultiDtoResponse>()
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) =>
                {
                    if (ctx.Items["userParticipation"] is not UserParticipation up) return (int?)null;
                    var userGrp = up.Groups.FirstOrDefault(g => g.MinigameGroupId == src.Id);
                    return userGrp?.Points;
                }));

            // --- Participations ---
            CreateMap<ParticipationExtraDto, ParticipationExtra>()
                .ForCtorParam("date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("season", opt => opt.MapFrom((src, ctx) => ctx.Items["season"]))
                .ForCtorParam("minigameGroupMatch2A", opt => opt.MapFrom(src => src.MinigameGroupMatch2A))
                .ForCtorParam("minigameGroupMatch2B", opt => opt.MapFrom(src => src.MinigameGroupMatch2B));
            CreateMap<ParticipationExtra, ParticipationExtraDtoResponse>()
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.HasPlayed, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items["userParticipation"] != null))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => (ctx.Items["userParticipation"] as UserParticipation)?.Points));

            CreateMap<ParticipationSpecialDto, ParticipationSpecial>()
                .ForCtorParam("date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("season", opt => opt.MapFrom((src, ctx) => ctx.Items["season"]))
                .ForCtorParam("minigameGroupMatch2A", opt => opt.MapFrom(src => src.MinigameGroupMatch2A))
                .ForCtorParam("minigameGroupMatch2B", opt => opt.MapFrom(src => src.MinigameGroupMatch2B));
            CreateMap<ParticipationSpecial, ParticipationSpecialDtoResponse>()
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.HasPlayed, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items["userParticipation"] != null))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => (ctx.Items["userParticipation"] as UserParticipation)?.Points));

            CreateMap<ParticipationStandartDto, ParticipationStandard>()
                .ForCtorParam("date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("season", opt => opt.MapFrom((src, ctx) => ctx.Items["season"]))
                .ForCtorParam("minigameGroupMulti", opt => opt.MapFrom(src => src.MinigameGroupMulti))
                .ForCtorParam("minigameGroupMatch3", opt => opt.MapFrom(src => src.MinigameGroupMatch3));
            CreateMap<ParticipationStandard, ParticipationStandartDtoResponse>()
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.HasPlayed, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items["userParticipation"] != null))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => (ctx.Items["userParticipation"] as UserParticipation)?.Points));
        }

        private static OptionInterval FromDto(OptionIntervalDto dto)
        {
            var price = ProbabilityPriceCalculator.GetPrice(dto.Probability);
            if (dto.Min.HasValue && dto.Max.HasValue)
                return OptionInterval.FromRange(price, dto.Min.Value, dto.Max.Value);
            if (dto.Min.HasValue)
                return OptionInterval.FromMin(price, dto.Min.Value);
            return OptionInterval.FromMax(price, dto.Max.Value);
        }
    }
}
