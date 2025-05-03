using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/seasons/{seasonId:int}/participations")]
    public class ParticipationsController : ControllerBase
    {
        private readonly IParticipationService _service;

        public ParticipationsController(IParticipationService service)  => _service = service;

        [HttpPost("standard")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateStandard(int seasonId, [FromBody] ParticipationStandartDto body)
        {
            var dto = await _service.CreateStandardAsync(seasonId, body);
            return CreatedAtAction
            (
                nameof(GetById),
                new { seasonId, participationId = dto.Id },
                dto
            );
        }

        [HttpPost("special")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateSpecial(int seasonId, [FromBody] ParticipationSpecialDto body)
        {
            var dto = await _service.CreateSpecialAsync(seasonId, body);
            return CreatedAtAction
            (
                nameof(GetById),
                new { seasonId, participationId = dto.Id },
                dto
            );
        }

        [HttpPost("extra")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateExtra(int seasonId, [FromBody] ParticipationExtraDto body)
        {
            var dto = await _service.CreateExtraAsync(seasonId, body);
            return CreatedAtAction
            (
                nameof(GetById),
                new { seasonId, participationId = dto.Id },
                dto
            );
        }

        [HttpPost("{participationId:int}/play")]
        [Authorize(Policy = "RequireUser")]
        public async Task<IActionResult> Play(int seasonId, int participationId, [FromBody] CreateUserParticipationDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _service.CreateUserParticipationAsync(seasonId, participationId, userId, dto);

            return CreatedAtAction(
                nameof(Play),
                new { seasonId, participationId, userParticipationId = result.Id },
                result
            );
        }

        [HttpPost("{participationId:int}/resolve")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> ResolveMinigame(int seasonId, int participationId, [FromBody] List<ParticipationResultDto> dtos) => Ok
        (
            await _service.ResolveMinigamesAsync(seasonId, participationId, dtos)
        );

        [HttpGet]
        public async Task<IActionResult> GetAll(int seasonId) => Ok(
            (await _service.GetBySeasonAsync(
                seasonId, 
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value, 
                User.IsInRole("Admin")
            )).Select(x => (object)x)
        );

        [HttpGet("{participationId:int}")]
        public async Task<IActionResult> GetById(int seasonId, int participationId) => Ok(
            await _service.GetByIdAsync(
                seasonId, 
                participationId,
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                User.IsInRole("Admin")
            )
        );

    }
}
