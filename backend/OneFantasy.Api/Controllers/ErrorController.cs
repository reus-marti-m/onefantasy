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

            // Typed exceptions
            if (ex is ApiException apiEx)
            {
                return Problem(
                   title: apiEx.Title,
                   detail: apiEx.Message,
                   statusCode: apiEx.StatusCode
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
