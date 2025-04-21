using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.Domain.Exceptions;
using OneFantasy.Api.Domain.Mappers;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneFantasy.Api.Domain.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _db;
        public PlayerService(AppDbContext db) => _db = db;

        public async Task<PlayerDtoResponse> CreateAsync(int teamId, PlayerDto dto)
        {
            var team = await _db.Teams.FindAsync(teamId) ?? throw new NotFoundException(nameof(Team), teamId);

            var player = dto.ToPlayer(team);
            _db.Players.Add(player);
            await _db.SaveChangesAsync();
            return player.ToDtoResponse();
        }

        public async Task<PlayerDtoResponse> UpdateAsync(int playerId, PlayerDto dto)
        {
            var player = await _db.Players.FindAsync(playerId) ?? throw new NotFoundException(nameof(Player), playerId);

            player.Name = dto.Name;
            await _db.SaveChangesAsync();
            return player.ToDtoResponse();
        }

        public async Task<IEnumerable<PlayerDtoResponse>> GetByTeamAsync(int teamId)
        {
            if (!await _db.Teams.AnyAsync(t => t.Id == teamId))
                throw new NotFoundException(nameof(Team), teamId);

            var teams = await _db.Players
                .Where(p => p.Team.Id == teamId)
                .Include(p => p.Team.Players)
                .ToListAsync();

            return teams.Select(t => t.ToDtoResponse());
        }

        public async Task<PlayerDtoResponse> GetByIdAsync(int playerId)
        {
            var player = await _db.Players.FindAsync(playerId);
            return player is null ? throw new NotFoundException(nameof(Player), playerId) : player.ToDtoResponse();
        }
    }
}
