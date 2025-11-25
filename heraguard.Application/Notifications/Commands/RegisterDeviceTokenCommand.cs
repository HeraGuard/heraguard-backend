using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Notifications.Commands;

public record RegisterDeviceTokenCommand(
    Guid UserId,
    string DeviceToken,
    string Platform
) : IRequest<Result>;