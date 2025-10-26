using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Dtos;
using heraguard.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var command = new LoginCommand(loginDto.Email, loginDto.Password);
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleErrorResult(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var command = new RegisterCommand(
            registerDto.Email,
            registerDto.Password,
            registerDto.Name,
            registerDto.LastName,
            registerDto.RoleId
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleErrorResult(result);
    }

    private IActionResult HandleErrorResult<T>(Result<T> result)
    {
        return result.Error.Type switch
        {
            ErrorType.Validation => BadRequest(new { message = result.Error.Description }),
            ErrorType.NotFound => NotFound(new { message = result.Error.Description }),
            ErrorType.Conflict => Conflict(new { message = result.Error.Description }),
            ErrorType.Unauthorized => Unauthorized(new { message = result.Error.Description }),
            ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden,
                new { message = result.Error.Description }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error interno del servidor" })
        };
    }
}