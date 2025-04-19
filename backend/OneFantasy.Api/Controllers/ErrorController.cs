using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static OneFantasy.Api.Domain.Exceptions.CompetitionExceptions;
using static OneFantasy.Api.Domain.Exceptions.GenericExceptions;

namespace OneFantasy.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {

        [HttpGet("/error"), HttpPost("/error")]
        public IActionResult HandleError()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            // 409 Conflict
            if (ex is DuplicateCompetitionException dup)
            {
                return Problem(
                    title: "Conflict",
                    detail: dup.Message,
                    statusCode: StatusCodes.Status409Conflict
                );
            }

            // 404 Not Found
            if (ex is NotFoundException nf)
            {
                return Problem(
                    title: "Not Found",
                    detail: nf.Message,
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            // 500 Internal Server Error fallback
            return Problem(
                title: "Internal Server Error",
                detail: "An unexpected error occurred. Please contact the administrator.",
                statusCode: StatusCodes.Status500InternalServerError);
        }

    }
}
