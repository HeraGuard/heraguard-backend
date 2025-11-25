using heraguard.Application.Notifications.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;


[ApiController]
[Route("api/medication-intake")]
public class MedicationIntakeController : BaseController
{
    private readonly IMediator _mediator;

    public MedicationIntakeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmIntake([FromBody] ConfirmIntakeRequest request)
    {
        var command = new ConfirmMedicationIntakeCommand(
            request.ScheduleId,
            request.ActualTime,
            request.ConfirmedByUserId,
            request.Notes
        );

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(new { message = "Medicamento confirmado" });

        return HandleErrorResult(result);
    }

    [HttpPost("skip")]
    public async Task<IActionResult> SkipMedication([FromBody] SkipMedicationRequest request)
    {
        var command = new SkipMedicationCommand(
            request.ScheduleId,
            request.UserId,
            request.Reason
        );

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            return Ok(new { message = "Medicamento omitido" });

        return HandleErrorResult(result);
    }
}

public record ConfirmIntakeRequest(
    Guid ScheduleId, 
    DateTime? ActualTime, 
    Guid ConfirmedByUserId, 
    string? Notes
);

public record SkipMedicationRequest(
    Guid ScheduleId, 
    Guid UserId, 
    string? Reason
);