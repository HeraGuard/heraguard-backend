using heraguard.Application.Medications.Commands;
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
}