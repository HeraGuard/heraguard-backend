using MediatR;
using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Domain.Common;
using System;

namespace heraguard.Application.MedicalAppointments.Queries;

public class GetMedicalAppointmentByIdQuery : IRequest<Result<ReadMedicalAppointmentDto>>
{
    public Guid MedicalAppointmentId { get; set; }

    public GetMedicalAppointmentByIdQuery(Guid medicalAppointmentId)
    {
        MedicalAppointmentId = medicalAppointmentId;
    }
}

