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
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public PlayerService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PlayerDtoResponse> CreateAsync(int teamId, PlayerDto dto)
        {
            var team = await _db.Teams.FindAsync(teamId) ?? throw new NotFoundException(nameof(Team), teamId);

            var player = _mapper.Map<Player>(dto, opts =>
            {
                opts.Items["Team"] = team;
            });
            _db.Players.Add(player);
            await _db.SaveChangesAsync();
            return _mapper.Map<PlayerDtoResponse>(player);
        }

        public async Task<PlayerDtoResponse> UpdateAsync(int playerId, PlayerDto dto)
        {
            var player = await _db.Players.FindAsync(playerId) ?? throw new NotFoundException(nameof(Player), playerId);

            player.Name = dto.Name;
            await _db.SaveChangesAsync();
            return _mapper.Map<PlayerDtoResponse>(player);
        }

        public async Task<IEnumerable<PlayerDtoResponse>> GetByTeamAsync(int teamId)
        {
            if (!await _db.Teams.AnyAsync(t => t.Id == teamId))
                throw new NotFoundException(nameof(Team), teamId);

            var teams = await _db.Players
                .Where(p => p.Team.Id == teamId)
                .Include(p => p.Team.Players)
                .ToListAsync();

            return teams.Select(_mapper.Map<PlayerDtoResponse>);
        }

        public async Task<PlayerDtoResponse> GetByIdAsync(int playerId)
        {
            var player = await _db.Players.FindAsync(playerId);
            return player is null ? throw new NotFoundException(nameof(Player), playerId) : _mapper.Map<PlayerDtoResponse>(player);
        }
    }
}
