using heraguard.Application.Sos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;


[ApiController]
[Route("api/sos")]
public class SosController : BaseController
{
    private readonly IMediator _mediator;

    public SosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSos([FromBody] CreateSosRequest request)
    {
        var command = new CreateSosEventCommand(
            request.ElderId,
            request.Notes
        );

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value); 

        return HandleErrorResult(result);
    }
}

public record CreateSosRequest(
    Guid ElderId,
    string? Notes
);