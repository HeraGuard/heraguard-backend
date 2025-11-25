using heraguard.Application.Notifications.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Notifications.Queries;

public class GetMedicationIntakeByIdQuery : IRequest<Result<MedicationIntakeDto>>
{
    public Guid ScheduleId { get; }

    public GetMedicationIntakeByIdQuery(Guid scheduleId)
    {
        ScheduleId = scheduleId;
    }
}