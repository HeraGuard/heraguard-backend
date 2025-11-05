using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Activity> UpdateActivityAsync(Activity activity)
    {
        _context.Activities.Update(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public async Task<Activity?> GetActivityByIdAsync(Guid activityId)
    {
        return await _context.Activities
            .FirstOrDefaultAsync(a => a.ActivityId == activityId);
    }

    public async Task<bool> DeleteActivityAsync(Guid activityId)
    {
        var deleted = await _context.Activities
            .Where(a => a.ActivityId == activityId)
            .ExecuteDeleteAsync();

        return deleted > 0;
    }

    public Task<List<Activity>> GetAllActivitiesByUserIdAsync(Guid userId)
    {
        return _context.Activities
            .Where(a => a.ElderId == userId)
            .Include(a => a.DoctorProfile)
            .ThenInclude(d => d.User)
            .Include(a => a.CaregiverProfile)
            .ThenInclude(c => c.User)
            .Include(a => a.ElderProfile)
            .ThenInclude(e => e.User)
            .ToListAsync();
    }

    public Task<Activity> GetActivityByIdWithRelationsAsync(Guid activityId)
    {
        return _context.Activities
            .Include(a => a.DoctorProfile)
            .ThenInclude(d => d.User)
            .Include(a => a.CaregiverProfile)
            .ThenInclude(c => c.User)
            .Include(a => a.ElderProfile)
            .ThenInclude(e => e.User)
            .FirstOrDefaultAsync(a => a.ActivityId == activityId);
    }

}