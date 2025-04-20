using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;
using System.Threading.Tasks;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/competitions/{competitionId:int}/seasons")]
    [Authorize(Policy = "RequireAdmin")]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonService _svc;
        public SeasonsController(ISeasonService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Post(int competitionId, SeasonDto dto)
        {
            var season = await _svc.CreateAsync(competitionId, dto);
            return CreatedAtAction(nameof(GetById),
                new { competitionId, seasonId = season.Id }, season);
        }

        [HttpPut("{seasonId:int}")]
        public async Task<IActionResult> Put(int seasonId, [FromBody] SeasonDto dto) => Ok(await _svc.UpdateAsync(seasonId, dto));

        [HttpGet]
        public async Task<IActionResult> GetAll(int competitionId) => Ok(await _svc.GetByCompetitionAsync(competitionId));

        [HttpGet("{seasonId:int}")]
        public async Task<IActionResult> GetById(int seasonId) => Ok(await _svc.GetByIdAsync(seasonId));
    }
}
