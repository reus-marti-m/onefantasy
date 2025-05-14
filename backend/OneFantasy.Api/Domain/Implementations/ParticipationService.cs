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
using OneFantasy.Api.Models.Participations.Users;
using Microsoft.AspNetCore.Identity;
using OneFantasy.Api.Models.Authentication;
using OneFantasy.Api.Models.Participations.MinigameGroups;

namespace OneFantasy.Api.Domain.Implementations
{
    public class ParticipationService : IParticipationService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _users;

        public ParticipationService(AppDbContext db, IMapper mapper, UserManager<ApplicationUser> users)
        {
            _db = db;
            _mapper = mapper;
            _users = users;
        }

        public async Task<ParticipationStandardDtoResponse> CreateStandardAsync(int seasonId, ParticipationStandardDto dto)
        {
            // Validations
            var season = await StandardValidations(seasonId, dto);

            // Validations ok
            var entity = _mapper.Map<ParticipationStandard>(dto, opts =>
            {
                opts.Items["season"] = season;
            });
            _db.Participations.Add(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<ParticipationStandardDtoResponse>(entity);
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

        public async Task<UserParticipationResponseDto> CreateUserParticipationAsync(
            int seasonId, int participationId, string userId, CreateUserParticipationDto dto)
        {
            // Validations
            (ApplicationUser user, Participation participation, int usedBudget) = await PlayValidations(seasonId, participationId, userId, dto);

            var existing = await _db.UserParticipations
                .Include(up => up.Groups)
                .FirstOrDefaultAsync(up => up.ParticipationId == participationId && up.UserId == userId);

            // Validations ok
            if (existing != null)
            {
                // Update
                _mapper.Map(dto, existing, opts =>
                {
                    opts.Items["user"] = user;
                    opts.Items["participation"] = participation;
                    opts.Items["usedBudget"] = usedBudget;
                });

                await _db.SaveChangesAsync();
                return _mapper.Map<UserParticipationResponseDto>(existing);
            }
            else
            {
                // Create
                var entity = _mapper.Map<UserParticipation>(dto, opts =>
                {
                    opts.Items["user"] = user;
                    opts.Items["participation"] = participation;
                    opts.Items["usedBudget"] = usedBudget;
                });
                _db.UserParticipations.Add(entity);
                await _db.SaveChangesAsync();
                return _mapper.Map<UserParticipationResponseDto>(entity);
            }
        }

        public async Task<List<IMinigameDtoResponse>> ResolveMinigamesAsync(int seasonId, int participationId, List<ParticipationResultDto> dtos)
        {
            // Pre validations
            var participation = await PreParticipationValidations(seasonId, participationId);

            // Validations, process and map
            var responseDtos = new List<IMinigameDtoResponse>();
            foreach (var dto in dtos)
            {
                var minigame = await _db.Set<Minigame>()
                    .Include(m => m.Options)
                    .Include(m => m.Group)
                    .FirstOrDefaultAsync(m =>
                        m.Id == dto.MinigameId &&
                        m.Group.ParticipationId == participationId)
                    ?? throw new NotFoundException(nameof(Minigame), dto.MinigameId);

                responseDtos.Add(ProcessValidateApplyAndMap(minigame, dto));
            }

            // Calculate scores for all users who have played.
            await ComputeUserScoresAsync(participation);

            // Validations and process ok
            await _db.SaveChangesAsync();
            return responseDtos;
        }

        public async Task<List<IParticipationDtoResponse>> GetBySeasonAsync(int seasonId, string userId, bool isAdmin)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(Season), seasonId);

            // Get participations
            var participations = await LoadFullParticipationsQuery()
                .Where(p => p.SeasonId == seasonId)
                .ToListAsync();

            // Load user info participation
            Dictionary<int, UserParticipation> userPlays = [];
            if (!isAdmin && !string.IsNullOrEmpty(userId))
            {
                userPlays = await _db.UserParticipations
                    .Include(up => up.Groups)
                        .ThenInclude(g => g.UserMinigames)
                            .ThenInclude(um => um.UserOptions)
                    .Where(up => up.Participation.SeasonId == seasonId
                              && up.UserId == userId)
                    .ToDictionaryAsync(up => up.ParticipationId);
            }

            // Add extra info to participations
            var result = participations
                .Select(p =>
                {
                    userPlays.TryGetValue(p.Id, out var up);
                    return MapParticipation(p, up);
                })
                .ToList();

            return result;
        }

        public async Task<IParticipationDtoResponse> GetByIdAsync(int seasonId, int participationId, string userId, bool isAdmin)
        {
            // Validations
            await PreParticipationValidations(seasonId, participationId);

            // Load user info participation
            UserParticipation up = null;
            if (!isAdmin && !string.IsNullOrEmpty(userId))
            {
                up = await _db.UserParticipations
                    .Include(up => up.Groups)
                        .ThenInclude(g => g.UserMinigames)
                            .ThenInclude(um => um.UserOptions)
                    .Where(up => up.Participation.SeasonId == seasonId
                              && up.UserId == userId && up.ParticipationId == participationId)
                    .FirstOrDefaultAsync();
            }

            // Validations ok
            return MapParticipation
            (
                await LoadFullParticipationsQuery()
                    .FirstOrDefaultAsync(p => p.Id == participationId),
                up,
                participationId
            );
        }

        #region "Helpers"
        private async Task ComputeUserScoresAsync(Participation participation)
        {
            int basePerMinigame, groupBonus, totalBonus;
            switch (participation)
            {
                case ParticipationStandard _:
                    basePerMinigame = 8; groupBonus = 3; totalBonus = 6;
                    break;
                case ParticipationExtra _:
                    basePerMinigame = 8; groupBonus = 2; totalBonus = 4;
                    break;
                case ParticipationSpecial _:
                    basePerMinigame = 16; groupBonus = 4; totalBonus = 8;
                    break;
                default:
                    throw new NotSupportedException();
            }

            // Get all user participations
            var userPlays = await _db.UserParticipations
                .Where(up => up.ParticipationId == participation.Id)
                .Include(up => up.Groups)
                    .ThenInclude(g => g.UserMinigames)
                        .ThenInclude(um => um.UserOptions)
                            .ThenInclude(uo => uo.Option)
                .Include(up => up.Groups)
                    .ThenInclude(g => g.UserMinigames)
                        .ThenInclude(um => um.Minigame)
                .ToListAsync();

            // Calculate scores for each user
            foreach (var up in userPlays)
            {
                int participationPoints = 0;
                bool allGroupsFullyCorrect = true;

                foreach (var grp in up.Groups)
                {
                    // Resolved minigames
                    var resolvedMgs = grp.UserMinigames
                        .Where(um => um.Minigame.IsResolved)
                        .ToList();

                    if (resolvedMgs.Count == 0)
                    {
                        grp.Points = 0;
                        allGroupsFullyCorrect = false;
                        continue;
                    }

                    // Successes
                    int correctCount = resolvedMgs
                        .Count(um => um.UserOptions.Any(uo => uo.Option.HasOccurred));

                    // Base points
                    int groupPoints = correctCount * basePerMinigame;

                    // Group bonus points
                    bool groupFullyCorrect = correctCount == resolvedMgs.Count;
                    if (groupFullyCorrect)
                        groupPoints += groupBonus;
                    else
                        allGroupsFullyCorrect = false;

                    grp.Points = groupPoints;
                    participationPoints += groupPoints;
                }

                // Participation bonus points
                if (allGroupsFullyCorrect)
                    participationPoints += totalBonus;

                up.Points = participationPoints;
            }
        }

        private async Task<(ApplicationUser u, Participation p, int usedBudget)> PlayValidations(int seasonId, int participationId, string userId, CreateUserParticipationDto dto)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(Season), seasonId);

            var participation = await _db.Participations
                .Include(p => p.Groups)
                    .ThenInclude(g => g.Minigames)
                        .ThenInclude(m => m.Options)
                .FirstOrDefaultAsync(p => p.Id == participationId && p.SeasonId == seasonId)
                ?? throw new NotFoundException(nameof(Participation), participationId);

            var user = await _users.FindByIdAsync(userId)
                       ?? throw new InvalidCredentialsException();

            var count = participation.Groups.Count;
            if (dto.Groups.Count != count)
                throw new ParticipationGroupsCountException(count);

            var groupMap = participation.Groups.ToDictionary(g => g.Id);

            int totalCost = 0;
            foreach (var ug in dto.Groups)
            {
                if (!groupMap.TryGetValue(ug.GroupId, out var templateGroup))
                    throw new NotFoundException(nameof(MinigameGroup), ug.GroupId);

                if (ug.Minigames.Count != templateGroup.Minigames.Count)
                    throw new ParticipationMinigamesCountException(ug.GroupId, ug.Minigames.Count);

                var mgMap = templateGroup.Minigames.ToDictionary(m => m.Id);

                foreach (var um in ug.Minigames)
                {
                    if (!mgMap.TryGetValue(um.MinigameId, out var templateMg))
                        throw new NotFoundException(nameof(Minigame), um.MinigameId);

                    if (um.SelectedOptionIds.Count != um.SelectedOptionIds.Distinct().Count())
                        throw new DuplicatedSelectedOptions(um.MinigameId);

                    var selectedOptions = um.SelectedOptionIds.Count;
                    if (selectedOptions == 0 || selectedOptions > 2)
                        throw new InvalidSelectedOptionNum(um.MinigameId, selectedOptions);

                    var optMap = templateMg.Options.ToDictionary(o => o.Id);
                    foreach (var optId in um.SelectedOptionIds)
                    {
                        if (!optMap.TryGetValue(optId, out var opt))
                            throw new OptionNotInMinigameException([optId], um.MinigameId);

                        totalCost += opt.Price;
                    }
                }
            }

            if (totalCost > participation.Budget)
                throw new ParticipationBudgetExceededException(participation.Budget, totalCost);

            return (user, participation, totalCost);
        }

        private async Task<Participation> PreParticipationValidations(int seasonId, int participationId)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(Season), seasonId);

            var participation = await _db.Participations
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == participationId) ?? throw new NotFoundException(nameof(Participation), participationId);

            if (participation.SeasonId != seasonId)
                throw new ParticipationNotInSeasonException(participationId, seasonId);

            return participation;
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
                    .ThenInclude(m => m.Options)
            .Include(p => p.Season)
                .ThenInclude(s => s.Competition);

        private IParticipationDtoResponse MapParticipation(Participation p, UserParticipation userParticipation, int? id = null)
        {
            return p switch
            {
                ParticipationStandard std => _mapper.Map<ParticipationStandardDtoResponse>(std, opts =>
                {
                    opts.Items["userParticipation"] = userParticipation;
                }),
                ParticipationSpecial sp => _mapper.Map<ParticipationSpecialDtoResponse>(sp, opts =>
                {
                    opts.Items["userParticipation"] = userParticipation;
                }),
                ParticipationExtra ex => _mapper.Map<ParticipationExtraDtoResponse>(ex, opts =>
                {
                    opts.Items["userParticipation"] = userParticipation;
                }),
                _ => id.HasValue ? throw new NotFoundException(nameof(Participation), id.Value) : null
            };
        }

        private async Task<Season> StandardValidations(int seasonId, ParticipationStandardDto dto)
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
