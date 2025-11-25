using System.Linq.Expressions;

namespace heraguard.Application.Notifications.Interfaces;

public interface IBackgroundJobScheduler
{
    string ScheduleJob<T>(Expression<Action<T>> methodCall, DateTime scheduledTime);
    void CancelJob(string jobId);
}