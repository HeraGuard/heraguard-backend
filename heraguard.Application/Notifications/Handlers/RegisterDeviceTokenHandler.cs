using heraguard.Application.Notifications.Commands;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Notifications.Handlers;


public class RegisterDeviceTokenHandler : IRequestHandler<RegisterDeviceTokenCommand, Result>
{
    private readonly IUserDeviceTokenRepository _repository;

    public RegisterDeviceTokenHandler(IUserDeviceTokenRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(RegisterDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByUserIdAsync(request.UserId);

        if (existing != null)
        {
            existing.DeviceToken = request.DeviceToken;
            existing.Platform = request.Platform;
            existing.UpdatedAt = DateTime.UtcNow;
            await _repository.AddOrUpdateAsync(existing);
        }
        else
        {
            var token = new UserDeviceToken
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                DeviceToken = request.DeviceToken,
                Platform = request.Platform,
                UpdatedAt = DateTime.UtcNow
            };
            await _repository.AddOrUpdateAsync(token);
        }

        return Result.Success();
    }
}