using OneFantasy.Api.DTOs;
using OneFantasy.Api.Domain.Exceptions;
using OneFantasy.Api.Models.Competitions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.Domain.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Implementations
{
    public class ParticipationService : IParticipationService
    {
        private readonly AppDbContext _db;
        public ParticipationService(AppDbContext db) => _db = db;

        public async Task<ParticipationStandartDtoResponse> CreateStandardAsync(int seasonId, ParticipationStandartDto dto)
        {
            // Gets
            var season = await LoadSeasonWithTeamsAsync(seasonId);
            var mg3 = dto.MinigameGroupMatch3;
            var home = GetTeamOrThrow(season, mg3.HomeTeamId);
            var visit = GetTeamOrThrow(season, mg3.VisitingTeamId);

            // Validate teams of multi
            foreach (var m in new[] { dto.MinigameGroupMulti.Match1,
                                      dto.MinigameGroupMulti.Match2,
                                      dto.MinigameGroupMulti.Match3 })
            {
                GetTeamOrThrow(season, m.HomeVictory.TeamId);
                GetTeamOrThrow(season, m.VisitingVictory.TeamId);
            }

            // Validate players 1
            ValidatePlayersInTeams(
                dto.MinigameGroupMatch3.MinigamePlayers1.Options.Select(o => o.PlayerId),
                home, visit
            );

            // Validate players 2
            ValidatePlayersInTeams(
                dto.MinigameGroupMatch3.MinigamePlayers2.Options.Select(o => o.PlayerId),
                home, visit
            );

            // Validations ok
            var entity = dto.CreateParticipationStandard(season);
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return entity.ToDtoResponse();
        }

        public async Task<ParticipationSpecialDtoResponse> CreateSpecialAsync(int seasonId, ParticipationSpecialDto dto)
        {
            // Validations
            var season = await ExtraAndSpecialValidations(
                dto.MinigameGroupMatch2A,
                dto.MinigameGroupMatch2B,
                seasonId
            );

            // Validations ok
            var entity = dto.CreateParticipationSpecial(season);
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return entity.ToDtoResponse();
        }

        public async Task<ParticipationExtraDtoResponse> CreateExtraAsync(int seasonId, ParticipationExtraDto dto)
        {
            // Validations
            var season = await ExtraAndSpecialValidations(
                dto.MinigameGroupMatch2A,
                dto.MinigameGroupMatch2B,
                seasonId
            );

            // Validations ok
            var entity = dto.CreateParticipationExtra(season);
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return entity.ToDtoResponse();
        }

        private async Task<Season> ExtraAndSpecialValidations(MinigameGroupMatch2ADto dto2A, MinigameGroupMatch2BDto dto2B, int seasonId)
        {
            // Gets
            var season = await LoadSeasonWithTeamsAsync(seasonId);
            var homeA = GetTeamOrThrow(season, dto2A.HomeTeamId);
            var visitA = GetTeamOrThrow(season, dto2A.VisitingTeamId);
            var homeB = GetTeamOrThrow(season, dto2B.HomeTeamId);
            var visitB = GetTeamOrThrow(season, dto2B.VisitingTeamId);

            // Validate unique scores
            ValidateUniqueScoreCombinations(
                dto2A.MinigameScores.Options
                     .Select(o => (o.HomeGoals, o.AwayGoals))
            );

            // Validate players 1
            ValidatePlayersInTeams(
                dto2A.MinigamePlayers.Options.Select(o => o.PlayerId),
                homeA, visitA
            );

            // Validate ntervals
            ValidateIntervals(dto2B.MinigameMatch.Options);

            // Validate players 2
            ValidatePlayersInTeams(
                dto2B.MinigamePlayers.Options.Select(o => o.PlayerId),
                homeB, visitB
            );

            return season;
        }


        #region "Validations"

        private async Task<Season> LoadSeasonWithTeamsAsync(int seasonId)
        {
            var season = await _db.Seasons
                .Include(s => s.Teams)
                .ThenInclude(t => t.Players)
                .FirstOrDefaultAsync(s => s.Id == seasonId);
            return season ?? throw new NotFoundException(nameof(Season), seasonId);
        }

        private static Team GetTeamOrThrow(Season season, int teamId) => 
            season.Teams.SingleOrDefault(t => t.Id == teamId) ?? throw new NotFoundException(nameof(Team), teamId);

        private static void ValidatePlayersInTeams(IEnumerable<int> playerIds, Team home, Team visit)
        {
            var valid = home.Players.Select(p => p.Id)
                          .Concat(visit.Players.Select(p => p.Id))
                          .ToHashSet();
            foreach (var id in playerIds)
            {
                if (!valid.Contains(id))
                    throw new PlayerNotInMatchTeamsException(id, home.Id, visit.Id);
            }
        }

        private static void ValidateUniqueScoreCombinations(IEnumerable<(int HomeGoals, int AwayGoals)> combos)
        {
            if (combos.Distinct().Count() != combos.Count())
                throw new DuplicateScoreCombinationsException();
        }

        private static void ValidateIntervals(IEnumerable<OptionIntervalDto> intervals)
        {
            foreach (var opt in intervals)
            {
                if (!opt.Min.HasValue && !opt.Max.HasValue)
                    throw new IntervalWithoutMinAndMaxException();
                if (opt.Min.HasValue && opt.Max.HasValue && opt.Min > opt.Max)
                    throw new InvalidInterval(opt.Min.Value, opt.Max.Value);
            }
        }

        #endregion 
    }
}
