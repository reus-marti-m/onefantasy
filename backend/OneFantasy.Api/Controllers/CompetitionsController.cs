using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionsController : ControllerBase
    {

        private readonly ICompetitionService _svc;
        public CompetitionsController(ICompetitionService svc) => _svc = svc;

        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Post([FromBody] CompetitionDto dto)
        {
            var comp = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = comp.Id }, comp);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompetitionDto dto)
        {
            var updated = await _svc.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => Ok(
            await _svc.GetByIdAsync(id)
        );

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(
            await _svc.GetAllAsync()
        );

    }
}
