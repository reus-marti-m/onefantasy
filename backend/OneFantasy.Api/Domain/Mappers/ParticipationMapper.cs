using OneFantasy.Api.Domain.Helpers;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;
using OneFantasy.Api.Models.Participations.Minigames;
using OneFantasy.Api.Models.Participations;

namespace OneFantasy.Api.Domain.Mappers
{
    public static class ParticipationMapper
    {

        #region "Participations"

        public static ParticipationStandard CreateParticipationStandard(this ParticipationStandartDto p, Season s) => new
        (
            p.Date,
            s,
            CreateGroupMulti(p.MinigameGroupMulti),
            CreateGroupMatch3(p.MinigameGroupMatch3)
        );

        public static ParticipationStandartDtoResponse ToDtoResponse(this ParticipationStandard p) => new()
        {
            Id = p.Id,
            Date = p.Date,
            MinigameGroupMulti = p.MinigameGroupMulti.ToDtoResponse(),
            MinigameGroupMatch3 = p.MinigameGroupMatch3.ToDtoResponse()
        };

        public static ParticipationSpecial CreateParticipationSpecial(this ParticipationSpecialDto p, Season s) => new
        (
            p.Date,
            s,
            CreateGroup2A(p.MinigameGroupMatch2A),
            CreateGroup2B(p.MinigameGroupMatch2B)
        );

        public static ParticipationSpecialDtoResponse ToDtoResponse(this ParticipationSpecial p) => new()
        {
            Id = p.Id,
            Date = p.Date,
            MinigameGroupMatch2A = p.MinigameGroupMatch2A.ToDtoResponse(),
            MinigameGroupMatch2B = p.MinigameGroupMatch2B.ToDtoResponse()
        };

        public static ParticipationExtra CreateParticipationExtra(this ParticipationExtraDto p, Season s) => new
        (
            p.Date,
            s,
            CreateGroup2A(p.MinigameGroupMatch2A),
            CreateGroup2B(p.MinigameGroupMatch2B)
        );

        public static ParticipationExtraDtoResponse ToDtoResponse(this ParticipationExtra p) => new()
        {
            Id = p.Id,
            Date = p.Date,
            MinigameGroupMatch2A = p.MinigameGroupMatch2A.ToDtoResponse(),
            MinigameGroupMatch2B = p.MinigameGroupMatch2B.ToDtoResponse()
        };

        #endregion


        #region "MinigameGroups"

        private static MinigameGroupMatch2A CreateGroup2A(MinigameGroupMatch2ADto dto) => new
        (
            CreateMinigameScores(dto.MinigameScores),
            CreateMinigamePlayers(dto.MinigamePlayers),
            dto.HomeTeamId,
            dto.VisitingTeamId
        );

        private static MinigameGroupMatch2B CreateGroup2B(MinigameGroupMatch2BDto dto) => new
        (
            CreateMinigameMatch(dto.MinigameMatch),
            CreateMinigamePlayers(dto.MinigamePlayers),
            dto.HomeTeamId,
            dto.VisitingTeamId
        );

        private static MinigameGroupMulti CreateGroupMulti(MinigameGroupMultiDto dto) => new
        (
            CreateMinigameResult(dto.Match1),
            CreateMinigameResult(dto.Match2),
            CreateMinigameResult(dto.Match3)
        );

        private static MinigameGroupMatch3 CreateGroupMatch3(MinigameGroupMatch3Dto dto) => new
        (
            CreateMinigameScores(dto.MinigameScores),
            CreateMinigamePlayers(dto.MinigamePlayers1),
            CreateMinigamePlayers(dto.MinigamePlayers2),
            dto.HomeTeamId,
            dto.VisitingTeamId
        );

        private static MinigameGroupMatch2ADtoResponse ToDtoResponse(this MinigameGroupMatch2A g) => new()
        {
            Id = g.Id,
            MinigameScores = g.MinigameScores.ToDtoResponse(),
            MinigamePlayers = g.MinigamePlayers.ToDtoResponse(),
            HomeTeamId = g.HomeTeamId,
            VisitingTeamId = g.VisitingTeamId
        };

        private static MinigameGroupMatch2BDtoResponse ToDtoResponse(this MinigameGroupMatch2B g) => new()
        {
            Id = g.Id,
            MinigameMatch = g.MinigameMatch.ToDtoResponse(),
            MinigamePlayers = g.MinigamePlayers.ToDtoResponse(),
            HomeTeamId = g.HomeTeamId,
            VisitingTeamId = g.VisitingTeamId
        };

        private static MinigameGroupMultiDtoResponse ToDtoResponse(this MinigameGroupMulti g) => new()
        {
            Id = g.Id,
            Match1 = g.Match1.ToDtoResponse(),
            Match2 = g.Match2.ToDtoResponse(),
            Match3 = g.Match3.ToDtoResponse()
        };

        private static MinigameGroupMatch3DtoResponse ToDtoResponse(this MinigameGroupMatch3 g) => new()
        {
            Id = g.Id,
            MinigameScores = g.MinigameScores.ToDtoResponse(),
            MinigamePlayers1 = g.MinigamePlayers1.ToDtoResponse(),
            MinigamePlayers2 = g.MinigamePlayers2.ToDtoResponse(),
            HomeTeamId = g.HomeTeamId,
            VisitingTeamId = g.VisitingTeamId
        };

        #endregion


        #region "Minigames"

        private static MinigameResult CreateMinigameResult(MinigameResultDto dto) => new
        (
            CreateOptionTeam(dto.HomeVictory),
            ProbabilityPriceCalculator.GetPrice(dto.Draw.Probability),
            CreateOptionTeam(dto.VisitingVictory)
        );

        private static MinigameScores CreateMinigameScores(MinigameScoresDto dto) => new
        (
            dto.Options.ConvertAll(CreateOptionScore)
        );

        private static MinigamePlayers CreateMinigamePlayers(MinigamePlayersDto dto) => new
        (
            dto.Options.ConvertAll(CreateOptionPlayer),
            dto.Type
        );

        private static MinigameMatch CreateMinigameMatch(MinigameMatchDto dto) => new
        (
            dto.Options.ConvertAll(CreateOptionInterval),
            dto.Type
        );

        private static MinigameResultDtoResponse ToDtoResponse(this MinigameResult m) => new()
        {
            Id = m.Id,
            Draw = m.Draw.ToDtoResponse(),
            HomeVictory = m.HomeVictory.ToDtoResponse(),
            VisitingVictory = m.VisitingVictory.ToDtoResponse()
        };

        private static MinigameMatchDtoResponse ToDtoResponse(this MinigameMatch m) => new()
        {
            Id = m.Id,
            Options = m.IntervalOptions.ConvertAll(o => (OptionIntervalDto)o.ToDtoResponse()),
            Type = m.Type
        };

        private static MinigameScoresDtoResponse ToDtoResponse(this MinigameScores s) => new()
        {
            Id = s.Id,
            Options = s.ScoreOptions.ConvertAll(o => (OptionScoreDto)o.ToDtoResponse())
        };

        private static MinigamePlayersDtoResponse ToDtoResponse(this MinigamePlayers p) => new()
        {
            Id = p.Id,
            Type = p.Type,
            Options = p.PlayerOptions.ConvertAll(o => (OptionPlayerDto)o.ToDtoResponse())
        };

        #endregion


        #region "Options"

        private static OptionInterval CreateOptionInterval(OptionIntervalDto dto)
        {
            if (dto.Min.HasValue && dto.Max.HasValue)
                return OptionInterval.FromRange(ProbabilityPriceCalculator.GetPrice(dto.Probability), dto.Min.Value, dto.Max.Value);
            else if (dto.Min.HasValue)
                return OptionInterval.FromMin(ProbabilityPriceCalculator.GetPrice(dto.Probability), dto.Min.Value);
            else
                return OptionInterval.FromMax(ProbabilityPriceCalculator.GetPrice(dto.Probability), dto.Max.Value);
        }

        private static OptionTeam CreateOptionTeam(OptionTeamDto dto) => new
        (
            ProbabilityPriceCalculator.GetPrice(dto.Probability),
            dto.TeamId
        );

        private static OptionScore CreateOptionScore(OptionScoreDto dto) => new
        (
            ProbabilityPriceCalculator.GetPrice(dto.Probability),
            dto.HomeGoals,
            dto.AwayGoals
        );

        private static OptionPlayer CreateOptionPlayer(OptionPlayerDto dto) => new
        (
            ProbabilityPriceCalculator.GetPrice(dto.Probability),
            dto.PlayerId
        );

        private static OptionDtoResponse ToDtoResponse(this Option o) => new()
        {
            Id = o.Id,
            Price = o.Price
        };

        private static OptionTeamDtoResponse ToDtoResponse(this OptionTeam o) => new()
        {
            Id = o.Id,
            Price = o.Price,
            TeamId = o.TeamId
        };

        private static OptionScoreDtoResponse ToDtoResponse(this OptionScore o) => new()
        {
            Id = o.Id,
            Price = o.Price,
            HomeGoals = o.HomeGoals,
            AwayGoals = o.AwayGoals
        };

        private static OptionPlayerDtoResponse ToDtoResponse(this OptionPlayer o) => new()
        {
            Id = o.Id,
            Price = o.Price,
            PlayerId = o.PlayerId
        };

        private static OptionIntervalDtoResponse ToDtoResponse(this OptionInterval o) => new()
        {
            Id = o.Id,
            Price = o.Price,
            Min = o.Min,
            Max = o.Max
        };

        #endregion

    }
}
