using heraguard.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;


[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleErrorResult<T>(Result<T> result)
    {
        return result.Error.Type switch
        {
            ErrorType.Validation => BadRequest(new { message = result.Error.Description }),
            ErrorType.NotFound => NotFound(new { message = result.Error.Description }),
            ErrorType.Conflict => Conflict(new { message = result.Error.Description }),
            ErrorType.Unauthorized => Unauthorized(new { message = result.Error.Description }),
            ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden,
                new { message = result.Error.Description }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Error interno del servidor" })
        };
    }
    
    protected IActionResult HandleErrorResult(Result result)
    {
        return result.Error.Type switch
        {
            ErrorType.Validation => BadRequest(new { message = result.Error.Description }),
            ErrorType.NotFound => NotFound(new { message = result.Error.Description }),
            ErrorType.Conflict => Conflict(new { message = result.Error.Description }),
            ErrorType.Unauthorized => Unauthorized(new { message = result.Error.Description }),
            ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden,
                new { message = result.Error.Description }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "Error interno del servidor" })
        };
    }
}
