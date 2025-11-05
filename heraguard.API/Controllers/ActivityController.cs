using heraguard.Application.Activities.Commands;
using heraguard.Application.Activities.Queries;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(Guid id, [FromBody] UpdateActivityCommand command)
    {
        if (id != command.ActivityId) return BadRequest("El Id en la ruta no coincide");
        var result = await _mediator.Send(command);
        if (result.IsSuccess) return Ok(result.Value);
        return HandleErrorResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        var comamnd = new DeleteActivityCommand(id);
        var result = await _mediator.Send(comamnd);
        if (result.IsSuccess) return NoContent();
        return HandleErrorResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(Guid id)
    {
        var query = new GetActivitByIdyQuery(id);
        var result = await _mediator.Send(query);
        if (result.IsSuccess) return Ok(result.Value);
        return HandleErrorResult(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllActivitiesByUserId(Guid userId)
    {
        var query = new GetAllActivitiesByUserIdQuery(userId);
        var result = await _mediator.Send(query);
        if (result.IsSuccess) return Ok(result.Value);
        return HandleErrorResult(result);
    }
}