using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Notifications.Commands;

public record ConfirmMedicationIntakeCommand(
    Guid ScheduleId,
    DateTime? ActualTime, // null = "ahora", o específica
    Guid ConfirmedByUserId,
    string? Notes
) : IRequest<Result>;