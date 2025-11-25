using heraguard.Application.Notifications.Dtos;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Application.Notifications.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Notifications.Handlers;

public class GetMedicationIntakeByIdHandler : IRequestHandler<GetMedicationIntakeByIdQuery, Result<MedicationIntakeDto>>
{
    private readonly IMedicationScheduleRepository _scheduleRepository;

    public GetMedicationIntakeByIdHandler(IMedicationScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Result<MedicationIntakeDto>> Handle(GetMedicationIntakeByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetWithMedicationByIdAsync(request.ScheduleId);
        if (schedule == null)
            return Result<MedicationIntakeDto>.Failure(ScheduleErrors.NotFound);
        
        var dto = new MedicationIntakeDto
        {
            ScheduleId = schedule.Id,
            MedicationId = schedule.MedicationId,
            ElderId = schedule.ElderId,
            ScheduledTime = schedule.ScheduledTime,
            Status = schedule.Status,
            MedicationName = schedule.Medication.Name,
            Dosage = schedule.Medication.Dosage,
            // Más campos según tu entity/model
        };

        return Result<MedicationIntakeDto>.Success(dto);
    }
}

