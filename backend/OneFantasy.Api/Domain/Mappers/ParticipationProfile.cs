using System.Linq;
using AutoMapper;
using OneFantasy.Api.Domain.Helpers;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Domain.Mappers
{
    public class ParticipationProfile : Profile
    {
        public ParticipationProfile()
        {
            CreateMap<ParticipationStandartDto, ParticipationStandard>()
                .ConstructUsing((dto, ctx) => new ParticipationStandard(
                    dto.Date,
                    (Season)ctx.Items["season"],
                    ctx.Mapper.Map<MinigameGroupMulti>(dto.MinigameGroupMulti),
                    ctx.Mapper.Map<MinigameGroupMatch3>(dto.MinigameGroupMatch3)
                ));
            CreateMap<ParticipationStandard, ParticipationStandartDtoResponse>();

            CreateMap<ParticipationSpecialDto, ParticipationSpecial>()
                .ConstructUsing((dto, ctx) => new ParticipationSpecial(
                    dto.Date,
                    (Season)ctx.Items["season"],
                    ctx.Mapper.Map<MinigameGroupMatch2A>(dto.MinigameGroupMatch2A),
                    ctx.Mapper.Map<MinigameGroupMatch2B>(dto.MinigameGroupMatch2B)
                ));
            CreateMap<ParticipationSpecial, ParticipationSpecialDtoResponse>();

            CreateMap<ParticipationExtraDto, ParticipationExtra>()
                .ConstructUsing((dto, ctx) => new ParticipationExtra(
                    dto.Date,
                    (Season)ctx.Items["season"],
                    ctx.Mapper.Map<MinigameGroupMatch2A>(dto.MinigameGroupMatch2A),
                    ctx.Mapper.Map<MinigameGroupMatch2B>(dto.MinigameGroupMatch2B)
                ));
            CreateMap<ParticipationExtra, ParticipationExtraDtoResponse>();

            CreateMap<MinigameGroupMultiDto, MinigameGroupMulti>()
                .ConstructUsing((dto, ctx) => new MinigameGroupMulti(
                    ctx.Mapper.Map<MinigameResult>(dto.Match1),
                    ctx.Mapper.Map<MinigameResult>(dto.Match2),
                    ctx.Mapper.Map<MinigameResult>(dto.Match3)
                ));
            CreateMap<MinigameGroupMulti, MinigameGroupMultiDtoResponse>();

            CreateMap<MinigameGroupMatch3Dto, MinigameGroupMatch3>()
                .ConstructUsing((dto, ctx) => new MinigameGroupMatch3(
                    ctx.Mapper.Map<MinigameScores>(dto.MinigameScores),
                    ctx.Mapper.Map<MinigamePlayers>(dto.MinigamePlayers1),
                    ctx.Mapper.Map<MinigamePlayers>(dto.MinigamePlayers2),
                    dto.HomeTeamId,
                    dto.VisitingTeamId
                ));
            CreateMap<MinigameGroupMatch3, MinigameGroupMatch3DtoResponse>();

            CreateMap<MinigameGroupMatch2ADto, MinigameGroupMatch2A>()
                .ConstructUsing((dto, ctx) => new MinigameGroupMatch2A(
                    ctx.Mapper.Map<MinigameScores>(dto.MinigameScores),
                    ctx.Mapper.Map<MinigamePlayers>(dto.MinigamePlayers),
                    dto.HomeTeamId,
                    dto.VisitingTeamId
                ));
            CreateMap<MinigameGroupMatch2A, MinigameGroupMatch2ADtoResponse>();

            CreateMap<MinigameGroupMatch2BDto, MinigameGroupMatch2B>()
                .ConstructUsing((dto, ctx) => new MinigameGroupMatch2B(
                    ctx.Mapper.Map<MinigameMatch>(dto.MinigameMatch),
                    ctx.Mapper.Map<MinigamePlayers>(dto.MinigamePlayers),
                    dto.HomeTeamId,
                    dto.VisitingTeamId
                ));
            CreateMap<MinigameGroupMatch2B, MinigameGroupMatch2BDtoResponse>();

            CreateMap<MinigameResultDto, MinigameResult>()
                .ConstructUsing((dto, ctx) => new MinigameResult(
                    ctx.Mapper.Map<OptionTeam>(dto.HomeVictory),
                    ProbabilityPriceCalculator.GetPrice(dto.Draw.Probability),
                    ctx.Mapper.Map<OptionTeam>(dto.VisitingVictory)
                ));
            CreateMap<MinigameResult, MinigameResultDtoResponse>();

            CreateMap<MinigameScoresDto, MinigameScores>()
                .ConstructUsing(dto => new MinigameScores(
                    dto.Options.Select(o => new OptionScore(
                        ProbabilityPriceCalculator.GetPrice(o.Probability),
                        o.HomeGoals,
                        o.AwayGoals
                    )).ToList()
                ));
            CreateMap<MinigameScores, MinigameScoresDtoResponse>();

            CreateMap<MinigamePlayersDto, MinigamePlayers>()
                .ConstructUsing(dto => new MinigamePlayers(
                    dto.Options.Select(o => new OptionPlayer(
                        ProbabilityPriceCalculator.GetPrice(o.Probability),
                        o.PlayerId
                    )).ToList(),
                    dto.Type
                ));
            CreateMap<MinigamePlayers, MinigamePlayersDtoResponse>();

            CreateMap<MinigameMatchDto, MinigameMatch>()
                .ConstructUsing(dto => new MinigameMatch(
                    dto.Options.Select(o => FromDto(o)).ToList(),
                    dto.Type
                ));
            CreateMap<MinigameMatch, MinigameMatchDtoResponse>();

            CreateMap<OptionIntervalDto, OptionInterval>()
                .ConstructUsing(dto => FromDto(dto));
            CreateMap<OptionInterval, OptionIntervalDtoResponse>();

            CreateMap<OptionTeamDto, OptionTeam>()
                .ConstructUsing(dto => new OptionTeam(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.TeamId
                ));
            CreateMap<OptionTeam, OptionTeamDtoResponse>();

            CreateMap<OptionScoreDto, OptionScore>()
                .ConstructUsing(dto => new OptionScore(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.HomeGoals,
                    dto.AwayGoals
                ));
            CreateMap<OptionScore, OptionScoreDtoResponse>();

            CreateMap<OptionPlayerDto, OptionPlayer>()
                .ConstructUsing(dto => new OptionPlayer(
                    ProbabilityPriceCalculator.GetPrice(dto.Probability),
                    dto.PlayerId
                ));
            CreateMap<OptionPlayer, OptionPlayerDtoResponse>();
        }

        private static OptionInterval FromDto(OptionIntervalDto dto)
        {
            var price = ProbabilityPriceCalculator.GetPrice(dto.Probability);

            if (dto.Min.HasValue && dto.Max.HasValue)
                return OptionInterval.FromRange(price, dto.Min.Value, dto.Max.Value);

            if (dto.Min.HasValue)
                return OptionInterval.FromMin(price, dto.Min.Value);
            else
                return OptionInterval.FromMax(price, dto.Max.Value);
        }
    }
}
