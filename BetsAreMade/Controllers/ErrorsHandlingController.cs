using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BetsAreMade.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsHandlingController : ControllerBase
    {
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
               detail: exceptionHandler.Error.StackTrace,
               title: exceptionHandler.Error.Message);
        }

        [Route("Error/{statusCode:int}")]
        [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            return StatusCode(statusCode, null);
        }
    }
}
