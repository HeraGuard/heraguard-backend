using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;


public class CaregiverAlertRepository : ICaregiverAlertRepository
{
    private readonly HeraGuardDbContext _context;

    public CaregiverAlertRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CaregiverAlert alert)
    {
        await _context.CaregiverAlerts.AddAsync(alert);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CaregiverAlert>> GetUnreadByCaregiverIdAsync(Guid caregiverId)
    {
        return await _context.CaregiverAlerts
            .Include(a => a.Elder).ThenInclude(e => e.User)
            .Include(a => a.Medication)
            .Where(a => a.CaregiverId == caregiverId && !a.IsRead)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(Guid alertId)
    {
        var alert = await _context.CaregiverAlerts.FindAsync(alertId);
        if (alert != null)
        {
            alert.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }
}