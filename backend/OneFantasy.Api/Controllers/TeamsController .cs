using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/seasons/{seasonId:int}/teams")]
    [Consumes("application/json")]
    [Produces("application/json", "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamSvc;
        public TeamsController(ITeamService teamSvc)
        {
            _teamSvc = teamSvc;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(TeamDtoResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(int seasonId, [FromBody] TeamDto dto)
        {
            var team = await _teamSvc.CreateAsync(seasonId, dto);
            var fullTeam = await _teamSvc.GetByIdAsync(team.Id);
            return CreatedAtAction(nameof(GetById),
                new { seasonId, teamId = fullTeam.Id }, fullTeam);
        }

        [HttpPut("{teamId:int}")]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(TeamDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int teamId, [FromBody] TeamDto dto)
        {
            var team = await _teamSvc.UpdateAsync(teamId, dto);
            return Ok(await _teamSvc.GetByIdAsync(team.Id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TeamDtoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int seasonId) => Ok(await _teamSvc.GetBySeasonAsync(seasonId));

        [HttpGet("{teamId:int}")]
        [ProducesResponseType(typeof(TeamDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int teamId) => Ok(await _teamSvc.GetByIdAsync(teamId));
    }
}
