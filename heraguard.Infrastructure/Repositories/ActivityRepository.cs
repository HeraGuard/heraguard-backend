using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Interfaces;
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

    public async Task<Activity> AddActivityAsync(Activity activity)
    {
        await _context.Activities.AddAsync(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public Task<Activity> UpdateActivityAsync(Activity activity)
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