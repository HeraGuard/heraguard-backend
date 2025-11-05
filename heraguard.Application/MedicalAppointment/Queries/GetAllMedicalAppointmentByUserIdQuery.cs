using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Domain.Common;
using MediatR;


namespace heraguard.Application.MedicalAppointments.Queries;

public class GetAllMedicalAppointmentsByUserIdQuery : IRequest<Result<List<ReadMedicalAppointmentDto>>>
{
    public Guid UserId { get; set; }

    public GetAllMedicalAppointmentsByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}

