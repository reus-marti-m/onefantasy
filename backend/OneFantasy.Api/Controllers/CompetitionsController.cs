using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.Domain.Exceptions;
using OneFantasy.Api.DTOs;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json", "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public class CompetitionsController : ControllerBase
    {

        private readonly ICompetitionService _svc;
        public CompetitionsController(ICompetitionService svc) => _svc = svc;

        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(CompetitionDtoResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CompetitionDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),         
                new { id = created.Id }, 
                created                  
            );
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(CompetitionDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] CompetitionDto dto) => Ok
        (
            await _svc.UpdateAsync(id, dto)
        );

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CompetitionDtoResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id) => Ok
        (
            await _svc.GetByIdAsync(id)
        );

        [HttpGet]
        [ProducesResponseType(typeof(List<CompetitionDtoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok
        (
            await _svc.GetAllAsync()
        );

    }
}
