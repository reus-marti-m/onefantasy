using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdmin")]
    public class CompetitionsController : ControllerBase
    {

        private readonly ICompetitionService _svc;
        public CompetitionsController(ICompetitionService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCompetitionDto dto)
        {
            var comp = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = comp.Id }, comp);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comp = await _svc.GetByIdAsync(id);
            if (comp is null) return NotFound();
            return Ok(comp);
        }

    }
}
