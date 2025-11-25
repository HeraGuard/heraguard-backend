using heraguard.Application.Notifications.Commands;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Notifications.Handlers;

public class SkipMedicationHandler : IRequestHandler<SkipMedicationCommand, Result>
{
    private readonly IMedicationScheduleRepository _scheduleRepository;
    private readonly IMedicationIntakeLogRepository _intakeLogRepository;

    public SkipMedicationHandler(
        IMedicationScheduleRepository scheduleRepository,
        IMedicationIntakeLogRepository intakeLogRepository)
    {
        _scheduleRepository = scheduleRepository;
        _intakeLogRepository = intakeLogRepository;
    }

    public async Task<Result> Handle(SkipMedicationCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.ScheduleId);
        if (schedule == null)
            return Result.Failure(ScheduleErrors.NotFound);

        schedule.Status = "skipped";
        await _scheduleRepository.UpdateAsync(schedule);

        var log = new MedicationIntakeLog
        {
            Id = Guid.NewGuid(),
            MedicationScheduleId = schedule.Id,
            MedicationId = schedule.MedicationId,
            ElderId = schedule.ElderId,
            ScheduledTime = schedule.ScheduledTime,
            ActualTime = null,
            Status = "skipped",
            ConfirmedByUserId = request.UserId,
            Notes = request.Reason,
            CreatedAt = DateTime.UtcNow
        };
        await _intakeLogRepository.AddAsync(log);

        return Result.Success();
    }
}