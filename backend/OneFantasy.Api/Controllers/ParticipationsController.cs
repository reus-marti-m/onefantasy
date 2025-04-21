using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;
using System.Threading.Tasks;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipationsController : ControllerBase
    {
        private readonly IParticipationService _service;

        public ParticipationsController(IParticipationService service)  => _service = service;

        [HttpPost("{seasonId}/standard")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateStandard([FromRoute] int seasonId, [FromBody] ParticipationStandartDto dto) => Ok
        (
            await _service.CreateStandardAsync(seasonId, dto)
        );

        [HttpPost("{seasonId}/special")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateSpecial([FromRoute] int seasonId, [FromBody] ParticipationSpecialDto dto) => Ok
        (
            await _service.CreateSpecialAsync(seasonId, dto)
        );

        [HttpPost("{seasonId}/extra")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateExtra([FromRoute] int seasonId, [FromBody] ParticipationExtraDto dto) => Ok
        (
            await _service.CreateExtraAsync(seasonId, dto)
        );

    }
}
