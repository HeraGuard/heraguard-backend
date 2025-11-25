using heraguard.Application.Medications.Interfaces;
using heraguard.Application.Notifications.Commands;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Notifications.Handlers;

public class ConfirmMedicationIntakeHandler : IRequestHandler<ConfirmMedicationIntakeCommand, Result>
{
    private readonly IMedicationScheduleRepository _scheduleRepository;
    private readonly IMedicationIntakeLogRepository _intakeLogRepository;
    private readonly IMedicationRepository _medicationRepository;
    private readonly INotificationService _notificationService;

    public ConfirmMedicationIntakeHandler(
        IMedicationScheduleRepository scheduleRepository,
        IMedicationIntakeLogRepository intakeLogRepository,
        IMedicationRepository medicationRepository,
        INotificationService notificationService)
    {
        _scheduleRepository = scheduleRepository;
        _intakeLogRepository = intakeLogRepository;
        _medicationRepository = medicationRepository;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(ConfirmMedicationIntakeCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(request.ScheduleId);
        if (schedule == null)
            return Result.Failure(ScheduleErrors.NotFound);

        // Marcar como confirmado
        schedule.Status = "confirmed";
        await _scheduleRepository.UpdateAsync(schedule);

        // Registrar en el log
        var log = new MedicationIntakeLog
        {
            Id = Guid.NewGuid(),
            MedicationScheduleId = schedule.Id,
            MedicationId = schedule.MedicationId,
            ElderId = schedule.ElderId,
            ScheduledTime = schedule.ScheduledTime,
            ActualTime = request.ActualTime ?? DateTime.UtcNow,
            Status = "taken",
            ConfirmedByUserId = request.ConfirmedByUserId,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };
        await _intakeLogRepository.AddAsync(log);

        // Generar siguiente notificación si hay menos de 3 pendientes
        var pendingCount = await _scheduleRepository.CountPendingByMedicationAsync(schedule.MedicationId);
        if (pendingCount < 3)
        {
            await _notificationService.ScheduleInitialNotificationsAsync(
                schedule.MedicationId, 
                schedule.ElderId
            );
        }

        return Result.Success();
    }
}