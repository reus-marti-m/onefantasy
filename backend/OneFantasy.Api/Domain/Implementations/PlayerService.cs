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
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _db;
        public PlayerService(AppDbContext db) => _db = db;

        public async Task<Player> CreateAsync(int teamId, PlayerDto dto)
        {
            var team = await _db.Teams.FindAsync(teamId)
                       ?? throw new NotFoundException(nameof(Team), teamId);

            var player = new Player(dto.Name) { Team = team };
            _db.Players.Add(player);
            await _db.SaveChangesAsync();
            return player;
        }

        public async Task<Player> UpdateAsync(int playerId, PlayerDto dto)
        {
            var player = await _db.Players.FindAsync(playerId)
                         ?? throw new NotFoundException(nameof(Player), playerId);

            player.Name = dto.Name;
            await _db.SaveChangesAsync();
            return player;
        }

        public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId)
        {
            if (!await _db.Teams.AnyAsync(t => t.Id == teamId))
                throw new NotFoundException(nameof(Team), teamId);

            return await _db.Players
                .Where(p => p.Team.Id == teamId)
                .ToListAsync();
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            var player = await _db.Players.FindAsync(playerId);
            return player ?? throw new NotFoundException(nameof(Player), playerId);
        }
    }
}
