using heraguard.Application.Notifications.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;


[ApiController]
[Route("api/users")]
public class DeviceTokenController : BaseController
{
    private readonly IMediator _mediator;

    public DeviceTokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{userId}/device-token")]
    public async Task<IActionResult> RegisterDeviceToken(
        Guid userId,
        [FromBody] RegisterTokenRequest request)
    {
        var command = new RegisterDeviceTokenCommand(
            userId,
            request.DeviceToken,
            request.Platform
        );

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(new { message = "Token registrado exitosamente" });

        return HandleErrorResult(result);
    }
}

public record RegisterTokenRequest(string DeviceToken, string Platform);