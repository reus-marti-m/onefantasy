using AutoMapper;
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
        private readonly IMapper _mapper;
        public TeamService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TeamDtoResponse> CreateAsync(int seasonId, TeamDto dto)
        {
            var season = await _db.Seasons.FindAsync(seasonId) ?? throw new NotFoundException(nameof(Season), seasonId);

            var team = _mapper.Map<Team>(dto, opts =>
            {
                opts.Items["Season"] = season;
            });
            season.Teams.Add(team);
            await _db.SaveChangesAsync();
            return _mapper.Map<TeamDtoResponse>(team);
        }

        public async Task<TeamDtoResponse> UpdateAsync(int teamId, TeamDto dto)
        {
            var team = await _db.Teams.FindAsync(teamId) ?? throw new NotFoundException(nameof(Team), teamId);

            if (await _db.Teams.AnyAsync(t => t.SeasonId == team.SeasonId && t.Name == dto.Name && t.Id != teamId))
            {
                throw new DuplicateException(nameof(Team), dto.Name);
            }

            team.Name = dto.Name;
            team.Abbreviation = dto.Abbreviation;
            await _db.SaveChangesAsync();
            return _mapper.Map<TeamDtoResponse>(team);
        }

        public async Task<IEnumerable<TeamDtoResponse>> GetBySeasonAsync(int seasonId)
        {
            if (!await _db.Seasons.AnyAsync(s => s.Id == seasonId))
                throw new NotFoundException(nameof(Season), seasonId);

            var teams = await _db.Teams
                .Where(t => t.SeasonId == seasonId)
                .Include(t => t.Players)
                .ToListAsync();

            return teams.Select(_mapper.Map<TeamDtoResponse>);
        }

        public async Task<TeamDtoResponse> GetByIdAsync(int teamId)
        {
            var team = await _db.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            return _mapper.Map<TeamDtoResponse>(team) ?? throw new NotFoundException(nameof(Team), teamId);
        }
    }
}
