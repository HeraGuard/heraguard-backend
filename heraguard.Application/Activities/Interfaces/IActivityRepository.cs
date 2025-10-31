using heraguard.Domain.Entities;

namespace heraguard.Application.Activities.Interfaces;

public interface IActivityRepository
{
    Task<Activity> AddActivityAsync(Activity activity);
    Task<Activity> UpdateActivityAsync(Activity activity);
    Task<Activity> GetActivityByIdAsync(Guid id);
    Task<Activity> DeleteActivityAsync(Guid id);
    Task<List<Activity>> GetAllActivitiesByUserIdAsync(Guid id);
}