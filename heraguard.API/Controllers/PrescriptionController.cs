using heraguard.Application.Prescriptions.Commands;
using heraguard.Application.Prescriptions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : BaseController
{
    private readonly IMediator _mediator;

    public PrescriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return HandleErrorResult(result);
    }
    
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicationById(Guid id)
    {
        var query = new GetPrescriptionByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
            return Ok(result.Value);
    
        return HandleErrorResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedication(Guid id)
    {
        var command = new DeletePrescriptionCommand(id);
        var result = await _mediator.Send(command);
    
        if (result.IsSuccess)
            return NoContent(); 
    
        return HandleErrorResult(result);
    }
}