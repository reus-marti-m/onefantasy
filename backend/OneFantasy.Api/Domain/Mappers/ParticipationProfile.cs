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
                .ForMember(dest => dest.IsPlayed, opt => opt.MapFrom((src, dest, _, ctx) => OptionIsPlayed(ctx, src)));

            CreateMap<OptionPlayerDto, OptionPlayer>()
                .ConstructUsing(dto => new OptionPlayer(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.PlayerId
                ));
            CreateMap<OptionPlayer, OptionPlayerDtoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.HasOccurred, opt => opt.MapFrom(src => src.HasOccurred))
                .ForMember(dest => dest.IsPlayed, opt => opt.MapFrom((src, dest, _, ctx) => OptionIsPlayed(ctx, src)));

            CreateMap<OptionScoreDto, OptionScore>()
                .ConstructUsing(dto => new OptionScore(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.HomeGoals,
                    dto.AwayGoals
                ));
            CreateMap<OptionScore, OptionScoreDtoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.HasOccurred, opt => opt.MapFrom(src => src.HasOccurred))
                .ForMember(dest => dest.IsPlayed, opt => opt.MapFrom((src, dest, _, ctx) => OptionIsPlayed(ctx, src)));

            CreateMap<OptionTeamDto, OptionTeam>()
                .ConstructUsing(dto => new OptionTeam(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.TeamId
                ));
            CreateMap<OptionTeam, OptionTeamDtoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.HasOccurred, opt => opt.MapFrom(src => src.HasOccurred))
                .ForMember(dest => dest.IsPlayed, opt => opt.MapFrom((src, dest, _, ctx) => OptionIsPlayed(ctx, src)));

            CreateMap<OptionIntervalDto, OptionInterval>()
                .ConstructUsing(dto => FromDto(dto));
            CreateMap<OptionInterval, OptionIntervalDtoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.HasOccurred, opt => opt.MapFrom(src => src.HasOccurred))
                .ForMember(dest => dest.IsPlayed, opt => opt.MapFrom((src, dest, _, ctx) => OptionIsPlayed(ctx, src)));

            // --- Minigames ---
            CreateMap<MinigameMatchDto, MinigameMatch>()
                .ConstructUsing(dto => new MinigameMatch(
                    dto.Options.Select(FromDto).ToList(),
                    dto.MiniGameMatchType
                ))
                .ForMember(dest => dest.Options, opt => opt.Ignore());
            CreateMap<MinigameMatch, MinigameMatchDtoResponse>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.IntervalOptions))
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameScore(ctx, src)));

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
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.PlayerOptions))
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameScore(ctx, src)));

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
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.ScoreOptions))
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameScore(ctx, src)));

            CreateMap<MinigameResultDto, MinigameResult>()
                .ForCtorParam("homeVictory", opt => opt.MapFrom(src => src.HomeVictory))
                .ForCtorParam("drawPrice", opt => opt.MapFrom(src => ProbabilityPriceCalculator.GetPrice(src.Draw.Probability)))
                .ForCtorParam("visitingVictory", opt => opt.MapFrom(src => src.VisitingVictory))
                .ForMember(dest => dest.Options, opt => opt.Ignore());
            CreateMap<MinigameResult, MinigameResultDtoResponse>()
                .ForMember(dest => dest.HomeVictory, opt => opt.MapFrom(src => src.HomeVictory))
                .ForMember(dest => dest.Draw, opt => opt.MapFrom(src => src.Draw))
                .ForMember(dest => dest.VisitingVictory, opt => opt.MapFrom(src => src.VisitingVictory))
                .ForMember(dest => dest.IsResolved, opt => opt.MapFrom(src => src.IsResolved))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameScore(ctx, src)));

            // --- Minigame Groups 2A / 2B / 3 / Multi ---
            CreateMap<MinigameGroupMatch2ADto, MinigameGroupMatch2A>()
                .ForCtorParam("minigameScores", opt => opt.MapFrom(src => src.MinigameScores))
                .ForCtorParam("minigamePlayers", opt => opt.MapFrom(src => src.MinigamePlayers))
                .ForCtorParam("homeTeamId", opt => opt.MapFrom(src => src.HomeTeamId))
                .ForCtorParam("visitingTeamId", opt => opt.MapFrom(src => src.VisitingTeamId));
            CreateMap<MinigameGroupMatch2A, MinigameGroupMatch2ADtoResponse>()
                .ForMember(d => d.MinigameScores, o => o.MapFrom(src => src.MinigameScores))
                .ForMember(d => d.MinigamePlayers, o => o.MapFrom(src => src.MinigamePlayers))
                .ForMember(d => d.HomeTeamId, o => o.MapFrom(src => src.HomeTeamId))
                .ForMember(d => d.VisitingTeamId, o => o.MapFrom(src => src.VisitingTeamId))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameGroupScore(ctx, src)));

            CreateMap<MinigameGroupMatch2BDto, MinigameGroupMatch2B>()
                .ForCtorParam("minigameMatch", opt => opt.MapFrom(src => src.MinigameMatch))
                .ForCtorParam("minigamePlayers", opt => opt.MapFrom(src => src.MinigamePlayers))
                .ForCtorParam("homeTeamId", opt => opt.MapFrom(src => src.HomeTeamId))
                .ForCtorParam("visitingTeamId", opt => opt.MapFrom(src => src.VisitingTeamId));
            CreateMap<MinigameGroupMatch2B, MinigameGroupMatch2BDtoResponse>()
                .ForMember(d => d.MinigameMatch, o => o.MapFrom(src => src.MinigameMatch))
                .ForMember(d => d.MinigamePlayers, o => o.MapFrom(src => src.MinigamePlayers))
                .ForMember(d => d.HomeTeamId, o => o.MapFrom(src => src.HomeTeamId))
                .ForMember(d => d.VisitingTeamId, o => o.MapFrom(src => src.VisitingTeamId))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameGroupScore(ctx, src)));

            CreateMap<MinigameGroupMatch3Dto, MinigameGroupMatch3>()
                .ForCtorParam("minigameScores", opt => opt.MapFrom(src => src.MinigameScores))
                .ForCtorParam("minigamePlayers1", opt => opt.MapFrom(src => src.MinigamePlayers1))
                .ForCtorParam("minigamePlayers2", opt => opt.MapFrom(src => src.MinigamePlayers2))
                .ForCtorParam("homeTeamId", opt => opt.MapFrom(src => src.HomeTeamId))
                .ForCtorParam("visitingTeamId", opt => opt.MapFrom(src => src.VisitingTeamId));
            CreateMap<MinigameGroupMatch3, MinigameGroupMatch3DtoResponse>()
                .ForMember(d => d.MinigameScores, o => o.MapFrom(src => src.MinigameScores))
                .ForMember(d => d.MinigamePlayers1, o => o.MapFrom(src => src.MinigamePlayers1))
                .ForMember(d => d.MinigamePlayers2, o => o.MapFrom(src => src.MinigamePlayers2))
                .ForMember(d => d.HomeTeamId, o => o.MapFrom(src => src.HomeTeamId))
                .ForMember(d => d.VisitingTeamId, o => o.MapFrom(src => src.VisitingTeamId))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameGroupScore(ctx, src)));

            CreateMap<MinigameGroupMultiDto, MinigameGroupMulti>()
                .ForCtorParam("match1", opt => opt.MapFrom(src => src.Match1))
                .ForCtorParam("match2", opt => opt.MapFrom(src => src.Match2))
                .ForCtorParam("match3", opt => opt.MapFrom(src => src.Match3));
            CreateMap<MinigameGroupMulti, MinigameGroupMultiDtoResponse>()
                .ForMember(d => d.Match1, o => o.MapFrom(src => src.Match1))
                .ForMember(d => d.Match2, o => o.MapFrom(src => src.Match2))
                .ForMember(d => d.Match3, o => o.MapFrom(src => src.Match3))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => MinigameGroupScore(ctx, src)));

            // --- Participations ---
            CreateMap<ParticipationExtraDto, ParticipationExtra>()
                .ForCtorParam("date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("season", opt => opt.MapFrom((src, ctx) => ctx.Items["season"]))
                .ForCtorParam("minigameGroupMatch2A", opt => opt.MapFrom(src => src.MinigameGroupMatch2A))
                .ForCtorParam("minigameGroupMatch2B", opt => opt.MapFrom(src => src.MinigameGroupMatch2B));
            CreateMap<ParticipationExtra, ParticipationExtraDtoResponse>()
                .ForMember(d => d.Date, o => o.MapFrom(src => src.Date))
                .ForMember(d => d.MinigameGroupMatch2A, o => o.MapFrom(src => src.MinigameGroupMatch2A))
                .ForMember(d => d.MinigameGroupMatch2B, o => o.MapFrom(src => src.MinigameGroupMatch2B))
                .ForMember(d => d.Competition, o => o.MapFrom(src => src.Season.Competition.Name))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.HasPlayed, opt => opt.MapFrom((src, dest, _, ctx) => ctx.TryGetItems(out var items) && items["userParticipation"] != null))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => ctx.TryGetItems(out var items) ? (items["userParticipation"] as UserParticipation)?.Points : null));

            CreateMap<ParticipationSpecialDto, ParticipationSpecial>()
                .ForCtorParam("date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("season", opt => opt.MapFrom((src, ctx) => ctx.Items["season"]))
                .ForCtorParam("minigameGroupMatch2A", opt => opt.MapFrom(src => src.MinigameGroupMatch2A))
                .ForCtorParam("minigameGroupMatch2B", opt => opt.MapFrom(src => src.MinigameGroupMatch2B));
            CreateMap<ParticipationSpecial, ParticipationSpecialDtoResponse>()
                .ForMember(d => d.Date, o => o.MapFrom(src => src.Date))
                .ForMember(d => d.MinigameGroupMatch2A, o => o.MapFrom(src => src.MinigameGroupMatch2A))
                .ForMember(d => d.MinigameGroupMatch2B, o => o.MapFrom(src => src.MinigameGroupMatch2B))
                .ForMember(d => d.Competition, o => o.MapFrom(src => src.Season.Competition.Name))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.HasPlayed, opt => opt.MapFrom((src, dest, _, ctx) => ctx.TryGetItems(out var items) && items["userParticipation"] != null))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => ctx.TryGetItems(out var items) ? (items["userParticipation"] as UserParticipation)?.Points : null));

            CreateMap<ParticipationStandardDto, ParticipationStandard>()
                .ForCtorParam("date", opt => opt.MapFrom(src => src.Date))
                .ForCtorParam("season", opt => opt.MapFrom((src, ctx) => ctx.Items["season"]))
                .ForCtorParam("minigameGroupMulti", opt => opt.MapFrom(src => src.MinigameGroupMulti))
                .ForCtorParam("minigameGroupMatch3", opt => opt.MapFrom(src => src.MinigameGroupMatch3));
            CreateMap<ParticipationStandard, ParticipationStandardDtoResponse>()
                .ForMember(d => d.Date, o => o.MapFrom(src => src.Date))
                .ForMember(d => d.MinigameGroupMulti, o => o.MapFrom(src => src.MinigameGroupMulti))
                .ForMember(d => d.MinigameGroupMatch3, o => o.MapFrom(src => src.MinigameGroupMatch3))
                .ForMember(d => d.Competition, o => o.MapFrom(src => src.Season.Competition.Name))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.HasPlayed, opt => opt.MapFrom((src, dest, _, ctx) => ctx.TryGetItems(out var items) && items["userParticipation"] != null))
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, _, ctx) => ctx.TryGetItems(out var items) ? (items["userParticipation"] as UserParticipation)?.Points : null));
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

        private static bool? OptionIsPlayed(ResolutionContext ctx, Option o)
        {
            if (!ctx.TryGetItems(out var items) || items["userParticipation"] is not UserParticipation up) return null;
            var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == o.Minigame.GroupId);
            var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == o.Minigame.Id);
            return userMg != null && userMg.UserOptions.Any(uo => uo.OptionId == o.Id);

        }

        private static int? MinigameScore(ResolutionContext ctx, Minigame m)
        {
            if (!ctx.TryGetItems(out var items) || items["userParticipation"] is not UserParticipation up) return null;
            var userGroup = up.Groups.FirstOrDefault(g => g.MinigameGroupId == m.GroupId);
            var userMg = userGroup?.UserMinigames.FirstOrDefault(um => um.MinigameId == m.Id);
            return userMg?.Points;
        }

        private static int? MinigameGroupScore(ResolutionContext ctx, MinigameGroup g)
        {
            if (!ctx.TryGetItems(out var items) || items["userParticipation"] is not UserParticipation up) return null;
            var userGrp = up.Groups.FirstOrDefault(ug => ug.MinigameGroupId == g.Id);
            return userGrp?.Points;
        }
    }
}
