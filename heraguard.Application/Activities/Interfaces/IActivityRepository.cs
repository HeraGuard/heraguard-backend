using heraguard.Domain.Entities;

public interface IActivityRepository
{
    Task<Activity> CreateActivityAsync(Activity activity);
    Task<Activity> UpdateActivityAsync(ActivityUpdateDto activityUpdateDto);
    Task<Activity> GetActivityByIdAsync(Guid id);
    Task<Activity> DeleteActivityAsync(Guid id);
    Task<List<Activity>> GetAllActivitiesByUserIdAsync(Guid id);
}