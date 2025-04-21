using OneFantasy.Api.Data;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Threading.Tasks;
using OneFantasy.Api.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace OneFantasy.Api.Domain.Implementations
{
    public class CompetitionService : ICompetitionService
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CompetitionService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CompetitionDtoResponse> CreateAsync(CompetitionDto dto)
        {
            if (await _db.Competitions.AnyAsync(c => c.Name == dto.Name))
                throw new DuplicateException(nameof(Competition), dto.Name);
            
            var comp = _mapper.Map<Competition>(dto);
            _db.Competitions.Add(comp);
            await _db.SaveChangesAsync();
            return _mapper.Map<CompetitionDtoResponse>(comp);
        }

        public async Task<CompetitionDtoResponse> UpdateAsync(int id, CompetitionDto dto)
        {
            var comp = await _db.Competitions.FindAsync(id)
                       ?? throw new NotFoundException(nameof(Competition), id);

            if (await _db.Competitions.AnyAsync(c => c.Name == dto.Name && c.Id != id))
                throw new DuplicateException(nameof(Competition), dto.Name);

            comp.Name = dto.Name;
            comp.Type = dto.Type;
            comp.Format = dto.Format;

            await _db.SaveChangesAsync();
            return _mapper.Map<CompetitionDtoResponse>(comp);
        }

        public async Task<CompetitionDtoResponse> GetByIdAsync(int id)
        {
            var comp = await _db.Competitions.FindAsync(id);
            return comp is null ? throw new NotFoundException(nameof(Competition), id) : _mapper.Map<CompetitionDtoResponse>(comp);
        }

        public async Task<IEnumerable<CompetitionDtoResponse>> GetAllAsync()
        {
            var comps = await _db.Competitions
                .Include(c => c.Seasons)
                .ToListAsync();

            return comps.Select(_mapper.Map<CompetitionDtoResponse>);
        }

    }
}
