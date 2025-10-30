using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Dtos;
using heraguard.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
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
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst("sub")?.Value ?? User.Identity?.Name;
        
        var command = new LogoutCommand(userId);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            _logger.LogInformation("User {UserId} logged out successfully", userId);
            return Ok(new { message = "Logout exitoso" });
        }

        return HandleErrorResult(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var command = new RefreshTokenCommand(refreshTokenDto.RefreshToken);
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleErrorResult(result);
    }
}