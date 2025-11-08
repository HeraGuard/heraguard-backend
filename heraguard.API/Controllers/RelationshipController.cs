using heraguard.Application.Relationships.Command;
using heraguard.Application.Relationships.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelationshipController : BaseController
{
    private readonly IMediator _mediator;

    public RelationshipController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRelationshipCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }
    
    [HttpGet("{relationshipId}")]
    public async Task<IActionResult> GetRelationshipById(Guid relationshipId)
    {
        var query = new GetRelationshipByIdQuery(relationshipId);
        var result = await _mediator.Send(query);
        
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }
    
    [HttpDelete("{relationshipId}")]
    public async Task<IActionResult> Delete(Guid relationshipId)
    {
        var command = new DeleteRelationshipCommand(relationshipId);
        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetRelationshipsByUserId(Guid userId, [FromQuery] int typeId)
    {
        var query = new GetRelationshipsByUserIdQuery(userId, typeId);
        var result = await _mediator.Send(query);
        
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }
}