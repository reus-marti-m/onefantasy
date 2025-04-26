using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static OneFantasy.Api.Models.Participations.Minigames.MinigameMatch;
using static OneFantasy.Api.Models.Participations.Minigames.MinigamePlayers;

namespace OneFantasy.Api.DTOs
{


    #region "Participations"

    public abstract class ParticipationDto
    {
        [Required]
        public DateTime Date { get; set; }
    }

    public class ParticipationStandartDto : ParticipationDto
    {
        [Required]
        public MinigameGroupMultiDto MinigameGroupMulti { get; set; }

        [Required]
        public MinigameGroupMatch3Dto MinigameGroupMatch3 { get; set; }
    }

    public class ParticipationStandartDtoResponse : ParticipationStandartDto
    {
        public int Id { get; set; }
        public new MinigameGroupMultiDtoResponse MinigameGroupMulti { get; set; }
        public new MinigameGroupMatch3DtoResponse MinigameGroupMatch3 { get; set; }
    }

    public class ParticipationExtraDto : ParticipationDto
    {
        [Required]
        public MinigameGroupMatch2ADto MinigameGroupMatch2A { get; set; }

        [Required]
        public MinigameGroupMatch2BDto MinigameGroupMatch2B { get; set; }
    }

    public class ParticipationExtraDtoResponse : ParticipationExtraDto
    {
        public int Id { get; set; }
        public new MinigameGroupMatch2ADtoResponse MinigameGroupMatch2A { get; set; }
        public new MinigameGroupMatch2BDtoResponse MinigameGroupMatch2B { get; set; }
    }

    public class ParticipationSpecialDto : ParticipationDto
    {
        [Required]
        public MinigameGroupMatch2ADto MinigameGroupMatch2A { get; set; }

        [Required]
        public MinigameGroupMatch2BDto MinigameGroupMatch2B { get; set; }
    }

    public class ParticipationSpecialDtoResponse : ParticipationSpecialDto
    {
        public int Id { get; set; }
        public new MinigameGroupMatch2ADtoResponse MinigameGroupMatch2A { get; set; }

        public new MinigameGroupMatch2BDtoResponse MinigameGroupMatch2B { get; set; }
    }

    #endregion


    #region "MinigameGroups"

    public class MinigameGroupMultiDto
    {
        [Required]
        public MinigameResultDto Match1 { get; set; }

        [Required]
        public MinigameResultDto Match2 { get; set; }

        [Required]
        public MinigameResultDto Match3 { get; set; }
    }

    public class MinigameGroupMultiDtoResponse : MinigameGroupMultiDto
    {
        public int Id { get; set; }

        public new MinigameResultDtoResponse Match1 { get; set; }

        public new MinigameResultDtoResponse Match2 { get; set; }


        public new MinigameResultDtoResponse Match3 { get; set; }
    }

    public class MinigameGroupMatch2ADto
    {
        [Required]
        public MinigameScoresDto MinigameScores { get; set; }

        [Required]
        public MinigamePlayersDto MinigamePlayers { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int VisitingTeamId { get; set; }
    }

    public class MinigameGroupMatch2ADtoResponse : MinigameGroupMatch2ADto
    {
        public int Id { get; set; }

        public new MinigameScoresDtoResponse MinigameScores { get; set; }

        public new MinigamePlayersDtoResponse MinigamePlayers { get; set; }
    }

    public class MinigameGroupMatch3Dto
    {
        [Required]
        public MinigameScoresDto MinigameScores { get; set; }

        [Required]
        public MinigamePlayersDto MinigamePlayers1 { get; set; }

        [Required]
        public MinigamePlayersDto MinigamePlayers2 { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int VisitingTeamId { get; set; }
    }

    public class MinigameGroupMatch3DtoResponse : MinigameGroupMatch3Dto
    {
        public int Id { get; set; }
        public new MinigameScoresDtoResponse MinigameScores { get; set; }
        public new MinigamePlayersDtoResponse MinigamePlayers1 { get; set; }
        public new MinigamePlayersDtoResponse MinigamePlayers2 { get; set; }
    }

    public class MinigameGroupMatch2BDto
    {
        [Required]
        public MinigameMatchDto MinigameMatch { get; set; }

        [Required]
        public MinigamePlayersDto MinigamePlayers { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int VisitingTeamId { get; set; }
    }

    public class MinigameGroupMatch2BDtoResponse : MinigameGroupMatch2BDto
    {
        public int Id { get; set; }
        public new MinigameMatchDtoResponse MinigameMatch { get; set; }
        public new MinigamePlayersDtoResponse MinigamePlayers { get; set; }
    }

    #endregion


    #region "Minigames"

    public class MinigameResultDto
    {
        [Required]
        public OptionDto Draw { get; set; }

        [Required]
        public OptionTeamDto HomeVictory { get; set; }

        [Required]
        public OptionTeamDto VisitingVictory { get; set; }
    }

    public class MinigameResultDtoResponse : MinigameResultDto
    {
        public int Id { get; set; }
        public bool IsResolved { get; set; }
        public new OptionTeamDtoResponse HomeVictory { get; set; }
        public new OptionTeamDtoResponse VisitingVictory { get; set; }
    }

    public class MinigameMatchDto
    {
        [Required]
        public List<OptionIntervalDto> Options { get; set; }

        [Required]
        public MinigameMatchType Type { get; set; }
    }

    public class MinigameMatchDtoResponse : MinigameMatchDto
    {
        public int Id { get; set; }
        public bool IsResolved { get; set; }
        public new List<OptionIntervalDtoResponse> Options { get; set; }
    }

    public class MinigameScoresDto
    {
        [Required]
        public List<OptionScoreDto> Options { get; set; }
    }

    public class MinigameScoresDtoResponse : MinigameScoresDto
    {
        public int Id { get; set; }
        public bool IsResolved { get; set; }
        public new List<OptionScoreDtoResponse> Options { get; set; }
    }

    public class MinigamePlayersDto
    {
        [Required]
        public List<OptionPlayerDto> Options { get; set; }

        [Required]
        public MinigamePlayersType Type { get; set; }
    }

    public class MinigamePlayersDtoResponse : MinigamePlayersDto
    {
        public int Id { get; set; }
        public bool IsResolved { get; set; }
        public new List<OptionPlayerDtoResponse> Options { get; set; }
    }

    #endregion


    #region "Options"

    public class OptionDto
    {
        [Required]
        [Range(0.01, 10)]
        public decimal Probability { get; set; }
    }

    public class OptionTeamDto : OptionDto
    {
        [Required]
        public int TeamId { get; set; }
    }

    public class OptionTeamDtoResponse : OptionTeamDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public bool HasOccurred { get; set; }
    }

    public class OptionScoreDto : OptionDto
    {
        [Required]
        [Range(0, 8)]
        public int HomeGoals { get; set; }

        [Required]
        [Range(0, 8)]
        public int AwayGoals { get; set; }
    }

    public class OptionScoreDtoResponse : OptionScoreDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public bool HasOccurred { get; set; }
    }

    public class OptionPlayerDto : OptionDto
    {
        [Required]
        public int PlayerId { get; set; }
    }

    public class OptionPlayerDtoResponse : OptionPlayerDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public bool HasOccurred { get; set; }
    }

    public class OptionIntervalDto : OptionDto
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }

    public class OptionIntervalDtoResponse : OptionIntervalDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public bool HasOccurred { get; set; }
    }

    #endregion


}
