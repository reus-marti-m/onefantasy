using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.DTOs;
using System.Threading.Tasks;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/seasons/{seasonId:int}/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamSvc;
        private readonly IPlayerService _playerSvc;
        public TeamsController(ITeamService teamSvc, IPlayerService playerSvc)
        {
            _teamSvc = teamSvc;
            _playerSvc = playerSvc;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Post(int seasonId, [FromBody] TeamDto dto)
        {
            await using var tx = await HttpContext.RequestServices.GetRequiredService<AppDbContext>().Database.BeginTransactionAsync();

            var team = await _teamSvc.CreateAsync(seasonId, dto);

            //if (dto.Players != null)
            //    foreach (var p in dto.Players)
            //        await _playerSvc.CreateAsync(team.Id, p);

            await tx.CommitAsync();

            var fullTeam = await _teamSvc.GetByIdAsync(team.Id);
            return CreatedAtAction(nameof(GetById),
                new { seasonId, teamId = fullTeam.Id }, fullTeam);
        }

        [HttpPut("{teamId:int}")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Put(int teamId, [FromBody] TeamDto dto)
        {
            await using var tx = await HttpContext.RequestServices.GetRequiredService<AppDbContext>().Database.BeginTransactionAsync();

            var team = await _teamSvc.UpdateAsync(teamId, dto);

            //if (dto.Players != null)
            //    foreach (var p in dto.Players)
            //        await _playerSvc.CreateAsync(team.Id, p);

            await tx.CommitAsync();

            return Ok(await _teamSvc.GetByIdAsync(team.Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int seasonId) => Ok(await _teamSvc.GetBySeasonAsync(seasonId));

        [HttpGet("{teamId:int}")]
        public async Task<IActionResult> GetById(int teamId) => Ok(await _teamSvc.GetByIdAsync(teamId));
    }
}
