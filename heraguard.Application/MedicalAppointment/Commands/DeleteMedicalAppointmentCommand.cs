using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.MedicalAppointments.Commands;

public class DeleteMedicalAppointmentCommand : IRequest<Result<Unit>>
{
    public Guid MedicalAppointmentId { get; set; }

    public DeleteMedicalAppointmentCommand(Guid medicalAppointmentId)
    {
        MedicalAppointmentId = medicalAppointmentId;
    }
}

