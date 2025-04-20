using OneFantasy.Api.Data;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;
using System.Threading.Tasks;
using OneFantasy.Api.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Domain.Exceptions;
using System.Collections.Generic;

namespace OneFantasy.Api.Domain.Implementations
{
    public class CompetitionService : ICompetitionService
    {

        private readonly AppDbContext _db;
        public CompetitionService(AppDbContext db) => _db = db;

        public async Task<Competition> CreateAsync(CompetitionDto dto)
        {
            if (await _db.Competitions.AnyAsync(c => c.Name == dto.Name))
                throw new DuplicateException(nameof(Competition), dto.Name);
            var comp = new Competition(dto.Name, dto.Type, dto.Format);
            _db.Competitions.Add(comp);
            await _db.SaveChangesAsync();
            return comp;
        }

        public async Task<Competition> UpdateAsync(int id, CompetitionDto dto)
        {
            var comp = await _db.Competitions.FindAsync(id)
                       ?? throw new NotFoundException(nameof(Competition), id);

            if (await _db.Competitions.AnyAsync(c => c.Name == dto.Name && c.Id != id))
                throw new DuplicateException(nameof(Competition), dto.Name);

            comp.Name = dto.Name;
            comp.Type = dto.Type;
            comp.Format = dto.Format;

            await _db.SaveChangesAsync();
            return comp;
        }

        public async Task<Competition> GetByIdAsync(int id)
        {
            var comp = await _db.Competitions.FindAsync(id);
            return comp is null ? throw new NotFoundException(nameof(Competition), id) : comp;
        }

        public async Task<IEnumerable<Competition>> GetAllAsync() => await _db.Competitions.ToListAsync();

    }
}
