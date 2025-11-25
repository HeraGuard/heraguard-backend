using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Notifications.Commands;

public record SkipMedicationCommand(
    Guid ScheduleId,
    Guid UserId,
    string? Reason
) : IRequest<Result>;