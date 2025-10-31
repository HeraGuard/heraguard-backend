using heraguard.Application.Medications.Commands;
using heraguard.Application.Medications.Queries;
using heraguard.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationController : BaseController
{
    private readonly IMediator _mediator;

    public MedicationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedication([FromBody] CreateMedicationCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMedication(Guid id, [FromBody] UpdateMedicationCommand command)
    {
        if (id != command.MedicationId)
            return BadRequest("El ID en la ruta no coincide con el comando");

        var result = await _mediator.Send(command);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicationById(Guid id)
    {
        var query = new GetMedicationByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
            return Ok(result.Value);
    
        return HandleErrorResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedication(Guid id)
    {
        var command = new DeleteMedicationCommand(id);
        var result = await _mediator.Send(command);
    
        if (result.IsSuccess)
            return NoContent(); 
    
        return HandleErrorResult(result);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllMedicationsByUserId(Guid userId)
    {
        var query = new GetAllMedicationsByUserIdQuery(userId);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
            return Ok(result.Value);
    
        return HandleErrorResult(result);
    }
}