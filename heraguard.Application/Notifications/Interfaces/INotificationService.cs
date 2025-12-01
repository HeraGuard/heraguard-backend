namespace heraguard.Application.Notifications.Interfaces;

public interface INotificationService
{
    Task ScheduleInitialNotificationsAsync(Guid medicationId, Guid elderId);
    Task SendNotificationAsync(Guid scheduleId);
    Task SendCriticalAlertAsync(Guid scheduleId);
    Task NotifyCaregiversAsync(Guid elderId, Guid medicationId, string message);
    Task SendTestNotificationAsync(string fcmToken);
    Task SendSosAlertAsync(Guid elderId, Guid sosId);

}