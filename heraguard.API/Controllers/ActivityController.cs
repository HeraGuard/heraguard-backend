using heraguard.Application.Activities.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityController : BaseController
{
    private readonly IMediator _mediator;
    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess) return Ok(result.Value);
        return HandleErrorResult(result);
    }
}