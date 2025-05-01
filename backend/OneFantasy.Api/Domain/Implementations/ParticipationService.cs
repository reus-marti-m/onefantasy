using OneFantasy.Api.DTOs;
using OneFantasy.Api.Domain.Exceptions;
using OneFantasy.Api.Models.Competitions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Participations.Minigames;
using System;

namespace OneFantasy.Api.Domain.Implementations
{
    public class ParticipationService : IParticipationService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ParticipationService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ParticipationStandartDtoResponse> CreateStandardAsync(int seasonId, ParticipationStandartDto dto)
        {
            // Validations
            var season = await StandartValidations(seasonId, dto);

            // Validations ok
            var entity = _mapper.Map<ParticipationStandard>(dto, opts =>
            {
                opts.Items["season"] = season;
            });
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<ParticipationStandartDtoResponse>(entity);
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
            var entity = _mapper.Map<ParticipationSpecial>(dto, opts =>
            {
                opts.Items["season"] = season;
            });
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<ParticipationSpecialDtoResponse>(entity);
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
            var entity = _mapper.Map<ParticipationExtra>(dto, opts =>
            {
                opts.Items["season"] = season;
            });
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<ParticipationExtraDtoResponse>(entity);
        }

        public async Task<IEnumerable<IMinigameDtoResponse>> ResolveMinigamesAsync(int seasonId, int participationId, List<ParticipationResultDto> dtos)
        {
            // Pre validations
            await PreParticipationValidations(seasonId, participationId);

            // Validations, process and map
            var responseDtos = new List<IMinigameDtoResponse>();
            foreach (var dto in dtos)
                responseDtos.Add
                (
                    ProcessValidateApplyAndMap
                    (
                        await _db.Set<Minigame>()
                            .Include(m => m.Options)
                            .Include(m => m.Group)
                            .FirstOrDefaultAsync
                            (
                                m =>
                                    m.Id == dto.MinigameId &&
                                    m.Group.ParticipationId == participationId
                            ) ?? throw new NotFoundException(nameof(Minigame), dto.MinigameId),
                        dto
                    )
                );

            // Validations ok
            await _db.SaveChangesAsync();
            return responseDtos;
        }

        public async Task<IEnumerable<IParticipationDtoResponse>> GetBySeasonAsync(int seasonId)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(Season), seasonId);

            var participations = await LoadFullParticipationsQuery().ToListAsync();
            var temp = participations
                .Select(p => MapParticipation(p))
                .Where(dto => dto is not null)
                .ToList();

            return temp;
        }

        public async Task<IParticipationDtoResponse> GetByIdAsync(int seasonId, int participationId)
        {
            // Validations
            await PreParticipationValidations(seasonId, participationId);

            // Validations ok
            return MapParticipation
            (
                await LoadFullParticipationsQuery()
                    .FirstOrDefaultAsync(p => p.Id == participationId),
                participationId
            );
        }


        #region "Helpers"

        private async Task PreParticipationValidations(int seasonId, int participationId)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(Season), seasonId);

            var participation = await _db.Participations
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == participationId) ?? throw new NotFoundException(nameof(Participation), participationId);

            if (participation.SeasonId != seasonId)
                throw new ParticipationNotInSeasonException(participationId, seasonId);
        }

        private IMinigameDtoResponse ProcessValidateApplyAndMap(Minigame minigame, ParticipationResultDto dto)
        {
            // Validate minigame options
            var invalidIds = dto.OcurredOptions.
                Except(
                    minigame.Options.
                        Select(o => o.Id).
                        ToHashSet()
                ).ToList();
            if (invalidIds.Count != 0)
                throw new OptionNotInMinigameException(invalidIds, minigame.Id);

            // Internal function to mark ocurred options and mark resolved minigame
            void MarkOptions()
            {
                foreach (var o in minigame.Options)
                    o.HasOccurred = dto.OcurredOptions.Contains(o.Id);
                minigame.IsResolved = true;
            }

            switch (minigame)
            {
                case MinigameResult mr:
                    if (dto.OcurredOptions.Count != 1)
                        throw new InvalidResultOfMinigame(nameof(MinigameResult), mr.Id);
                    MarkOptions();
                    return _mapper.Map<MinigameResultDtoResponse>(mr);
                case MinigameMatch mm:
                    if (dto.OcurredOptions.Count != 1)
                        throw new InvalidResultOfMinigame(nameof(MinigameResult), mm.Id);
                    MarkOptions();
                    return _mapper.Map<MinigameMatchDtoResponse>(mm);
                case MinigamePlayers mp:
                    MarkOptions();
                    return _mapper.Map<MinigamePlayersDtoResponse>(mp);
                case MinigameScores ms:
                    if (dto.OcurredOptions.Count != 1)
                        throw new InvalidResultOfMinigame(nameof(MinigameResult), ms.Id);
                    MarkOptions();
                    return _mapper.Map<MinigameScoresDtoResponse>(ms);
                default:
                    throw new NotSupportedException();
            }
        }

        private IQueryable<Participation> LoadFullParticipationsQuery() => _db.Participations
            .Include(p => p.Groups)
                .ThenInclude(g => g.Minigames)
                    .ThenInclude(m => m.Options);

        private IParticipationDtoResponse MapParticipation(Participation p, int? id = null)
        {
            return p switch
            {
                ParticipationStandard std => _mapper.Map<ParticipationStandartDtoResponse>(std),
                ParticipationSpecial sp => _mapper.Map<ParticipationSpecialDtoResponse>(sp),
                ParticipationExtra ex => _mapper.Map<ParticipationExtraDtoResponse>(ex),
                _ => id.HasValue ? throw new NotFoundException(nameof(Participation), id.Value) : null
            };
        }

        private async Task<Season> StandartValidations(int seasonId, ParticipationStandartDto dto)
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

            return season;
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
