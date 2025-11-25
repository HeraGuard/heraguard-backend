using System.Linq.Expressions;
using Hangfire;
using heraguard.Application.Notifications.Interfaces;

namespace heraguard.Infrastructure.BackgroundJobs;

public class HangfireJobScheduler : IBackgroundJobScheduler
{
    public string ScheduleJob<T>(Expression<Action<T>> methodCall, DateTime scheduledTime)
    {
        return BackgroundJob.Schedule(methodCall, scheduledTime);
    }

    public void CancelJob(string jobId)
    {
        BackgroundJob.Delete(jobId);
    }
}