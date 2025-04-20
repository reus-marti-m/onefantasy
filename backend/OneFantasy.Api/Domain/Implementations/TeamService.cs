using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.Domain.Exceptions;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _db;
        public TeamService(AppDbContext db) => _db = db;

        public async Task<Team> CreateAsync(int seasonId, TeamDto dto)
        {
            var season = await _db.Seasons.FindAsync(seasonId) ?? throw new NotFoundException(nameof(CompetitionSeason), seasonId);

            var team = new Team(dto.Name, dto.Abbreviation, []);
            season.Teams.Add(team);
            await _db.SaveChangesAsync();
            return team;
        }

        public async Task<Team> UpdateAsync(int teamId, TeamDto dto)
        {
            var team = await _db.Teams.FindAsync(teamId) ?? throw new NotFoundException(nameof(Team), teamId);

            if (await _db.Teams.AnyAsync(t => t.CompetitionSeasonId == team.CompetitionSeasonId && t.Name == dto.Name && t.Id != teamId))
            {
                throw new DuplicateException(nameof(Team), dto.Name);
            }

            team.Name = dto.Name;
            team.Abbreviation = dto.Abbreviation;
            await _db.SaveChangesAsync();
            return team;
        }

        public async Task<IEnumerable<Team>> GetBySeasonAsync(int seasonId)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(CompetitionSeason), seasonId);

            return await _db.Teams
                .Where(t => t.CompetitionSeason.Id == seasonId)
                .Include(t => t.Players)
                .ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int teamId)
        {
            var team = await _db.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return team ?? throw new NotFoundException(nameof(Team), teamId);
        }
    }
}
