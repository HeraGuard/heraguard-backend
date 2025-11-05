using heraguard.Application.MedicalAppointments.Commands;
using heraguard.Application.MedicalAppointments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace heraguard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalAppointmentController : BaseController
{
    private readonly IMediator _mediator;

    public MedicalAppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedicalAppointment([FromBody] CreateMedicalAppointmentCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest("Error creating medical appointment.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMedicalAppointment(Guid id, [FromBody] UpdateMedicalAppointmentCommand command)
    {
        if (id != command.MedicalAppointmentId)
            return BadRequest("El ID en la ruta no coincide con el comando");

        var result = await _mediator.Send(command);
        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest("Error updating medical appointment.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicalAppointment(Guid id)
    {
        var command = new DeleteMedicalAppointmentCommand(id);
        var result = await _mediator.Send(command);
        if (result.IsSuccess) return NoContent();
        return BadRequest("Error deleting medical appointment.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicalAppointmentById(Guid id)
    {
        var query = new GetMedicalAppointmentByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result.IsSuccess) return Ok(result.Value);
        return NotFound("Medical appointment not found.");
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllMedicalAppointmentsByUserId(Guid userId)
    {
        var query = new GetAllMedicalAppointmentsByUserIdQuery(userId);
        var result = await _mediator.Send(query);
        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest("Error fetching medical appointments.");
    }
}

