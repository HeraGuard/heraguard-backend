using AutoMapper;
using heraguard.Application.MedicalAppointments.Commands;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.MedicalAppointments.Handlers;

public class DeleteMedicalAppointmentHandler : IRequestHandler<DeleteMedicalAppointmentCommand, Result<Unit>>
{
    private readonly IMedicalAppointmentRepository _repository;

    public DeleteMedicalAppointmentHandler(IMedicalAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(DeleteMedicalAppointmentCommand request, CancellationToken cancellationToken)
    {
        var medicalAppointment = await _repository.GetMedicalAppointmentByIdAsync(request.MedicalAppointmentId);
        if (medicalAppointment == null)
            return Result<Unit>.Failure(MedicalAppointmentErrors.NotFound);
        await _repository.DeleteMedicalAppointmentAsync(medicalAppointment);

        return Result<Unit>.Success(Unit.Value);
    }
}

