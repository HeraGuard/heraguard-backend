using heraguard.Domain.Entities;

namespace heraguard.Application.Activities.Interfaces;

public interface IActivityRepository
{
    Task<Activity> AddActivityAsync(Activity activity);
    Task<Activity> UpdateActivityAsync(Activity activity);
    Task<Activity?> GetActivityByIdAsync(Guid activityId);
    Task<bool> DeleteActivityAsync(Guid activityId);
    Task<List<Activity>> GetAllActivitiesByUserIdAsync(Guid userId);
    Task<Activity> GetActivityByIdWithRelationsAsync(Guid activityId);
}