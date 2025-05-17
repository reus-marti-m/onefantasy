using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.Domain.Exceptions;
using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/competitions/{competitionId:int}/seasons")]
    [Authorize(Policy = "RequireAdmin")]
    [Consumes("application/json")]
    [Produces("application/json", "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonService _svc;
        public SeasonsController(ISeasonService svc) => _svc = svc;

        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(SeasonDtoResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(int competitionId, SeasonDto dto)
        {
            var created = await _svc.CreateAsync(competitionId, dto);
            return CreatedAtAction
            (
                nameof(GetById),
                new { competitionId, seasonId = created.Id },
                created
            );
        }

        [HttpPut("{seasonId:int}")]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(SeasonDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int seasonId, [FromBody] SeasonDto dto) => Ok
        (
            await _svc.UpdateAsync(seasonId, dto)
        );

        [HttpGet]
        [ProducesResponseType(typeof(List<SeasonDtoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int competitionId) => Ok
        (
            await _svc.GetByCompetitionAsync(competitionId)
        );

        [HttpGet("{seasonId:int}")]
        [ProducesResponseType(typeof(SeasonDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int seasonId) => Ok
        (
            await _svc.GetByIdAsync(seasonId)
        );
    }
}
