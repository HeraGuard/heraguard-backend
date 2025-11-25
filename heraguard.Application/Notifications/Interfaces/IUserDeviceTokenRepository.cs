using heraguard.Domain.Entities;

namespace heraguard.Application.Notifications.Interfaces;

public interface IUserDeviceTokenRepository
{
    Task<List<string>> GetDeviceTokensByUserIdAsync(Guid userId);
    Task AddOrUpdateAsync(UserDeviceToken token);
    Task RemoveInvalidTokenAsync(string deviceToken);
    Task<UserDeviceToken?> GetByUserIdAsync(Guid userId);
}