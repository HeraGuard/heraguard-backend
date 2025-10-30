using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;

namespace heraguard.Infrastructure.Repositories;

public class ActivityRepository : IActivityRepository
{
    private readonly HeraGuardDbContext _context;

    public ActivityRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public Task<Activity> CreateActivityAsync(Activity activity)
    {
        throw new NotImplementedException();
    }

    public Task<Activity> UpdateActivityAsync(ActivityUpdateDto activityUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task<Activity> GetActivityByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Activity> DeleteActivityAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Activity>> GetAllActivitiesByUserIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

}