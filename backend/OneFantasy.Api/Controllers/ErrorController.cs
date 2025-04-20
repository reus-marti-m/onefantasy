using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneFantasy.Api.Domain.Exceptions;


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
            if (ex is DuplicateException || ex is DuplicateSeasonException)
            {
                return Problem(
                    title: "Conflict",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status409Conflict
                );
            }

            // 404 Not Found
            if (ex is NotFoundException)
            {
                return Problem(
                    title: "Not Found",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            // 401 Unauthorized for invalid credentials
            if (ex is InvalidCredentialsException)
            {
                return Problem(
                    title: "Invalid Credentials",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status401Unauthorized
                );
            }

            // 403 Forbidden for unauthorized admin actions
            if (ex is UnauthorizedAdminException ua)
            {
                return Problem(
                    title: "Forbidden",
                    detail: ua.Message,
                    statusCode: StatusCodes.Status403Forbidden
                );
            }

            // 500 Internal Server Error fallback
            return Problem(
                title: "Internal Server Error",
                detail: "An unexpected error occurred. Please contact the administrator.",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

    }
}
