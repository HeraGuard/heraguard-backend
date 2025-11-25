using heraguard.Domain.Entities;

namespace heraguard.Application.Notifications.Interfaces;

public interface ICaregiverAlertRepository
{
    Task AddAsync(CaregiverAlert alert);
    Task<List<CaregiverAlert>> GetUnreadByCaregiverIdAsync(Guid caregiverId);
    Task MarkAsReadAsync(Guid alertId);
}