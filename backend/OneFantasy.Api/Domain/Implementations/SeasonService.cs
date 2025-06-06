﻿using AutoMapper;
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
    public class SeasonService : ISeasonService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public SeasonService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SeasonDtoResponse> CreateAsync(int competitionId, SeasonDto dto)
        {
            var comp = await _db.Competitions.FindAsync(competitionId) ?? throw new NotFoundException(nameof(Competition), competitionId);

            var season = _mapper.Map<Season>(dto, opts =>
            {
                opts.Items["competition"] = comp;
            });
            _db.Seasons.Add(season);
            await _db.SaveChangesAsync();
            return _mapper.Map<SeasonDtoResponse>(season);
        }

        public async Task<SeasonDtoResponse> UpdateAsync(int seasonId, SeasonDto dto)
        {
            var season = await _db.Seasons
                .Include(s => s.Competition)
                .FirstOrDefaultAsync(s => s.Id == seasonId) ?? throw new NotFoundException(nameof(Season), seasonId);

            var compId = season.Competition.Id;
            if (await _db.Seasons.AnyAsync(s => s.Competition.Id == compId && s.Year == dto.Year && s.Id != seasonId))
                throw new DuplicateSeasonException(compId, dto.Year);

            season.Year = dto.Year;
            await _db.SaveChangesAsync();
            return _mapper.Map<SeasonDtoResponse>(season);
        }

        public async Task<List<SeasonDtoResponse>> GetByCompetitionAsync(int competitionId)
        {
            if (!await _db.Competitions.AnyAsync(c => c.Id == competitionId))
                throw new NotFoundException(nameof(Competition), competitionId);

            var comp = await _db.Seasons
                .Where(s => s.Competition.Id == competitionId)
                .Include(s => s.Competition)
                .Include(s => s.Teams)
                .ToListAsync();

            return [.. comp.Select(_mapper.Map<SeasonDtoResponse>)];
        }

        public async Task<SeasonDtoResponse> GetByIdAsync(int seasonId)
        {
            var season = await _db.Seasons
                .Include(s => s.Competition)
                .Include(s => s.Teams)
                .FirstOrDefaultAsync(s => s.Id == seasonId);

            return _mapper.Map<SeasonDtoResponse>(season) ?? throw new NotFoundException(nameof(Season), seasonId);
        }
    }
}
