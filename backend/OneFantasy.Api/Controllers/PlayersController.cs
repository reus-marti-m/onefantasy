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
    [Route("api/teams/{teamId:int}/players")]
    [Consumes("application/json")]
    [Produces("application/json", "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _svc;

        public PlayersController(IPlayerService svc) => _svc = svc;

        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(PlayerDtoResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(int teamId, PlayerDto dto)
        {
            var created = await _svc.CreateAsync(teamId, dto);
            return CreatedAtAction
            (
                nameof(GetById),
                new { teamId, playerId = created.Id },
                created
            );
        }

        [HttpPut("{playerId:int}")]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(PlayerDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int playerId, [FromBody] PlayerDto dto) => Ok
        (
            await _svc.UpdateAsync(playerId, dto)
        );

        [HttpGet]
        [ProducesResponseType(typeof(List<PlayerDtoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int teamId) => Ok
        (
            await _svc.GetByTeamAsync(teamId)
        );

        [HttpGet("{playerId:int}")]
        [ProducesResponseType(typeof(PlayerDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int playerId) => Ok
        (
            await _svc.GetByIdAsync(playerId)
        );
    }

}
