using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;


public class MedicationIntakeLogRepository : IMedicationIntakeLogRepository
{
    private readonly HeraGuardDbContext _context;

    public MedicationIntakeLogRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MedicationIntakeLog log)
    {
        await _context.MedicationIntakeLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MedicationIntakeLog>> GetByElderIdAsync(
        Guid elderId, 
        DateTime? startDate, 
        DateTime? endDate)
    {
        var query = _context.MedicationIntakeLogs
            .Include(l => l.Medication)
            .Include(l => l.ConfirmedBy)
            .Where(l => l.ElderId == elderId);

        if (startDate.HasValue)
            query = query.Where(l => l.ScheduledTime >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(l => l.ScheduledTime <= endDate.Value);

        return await query.OrderByDescending(l => l.ScheduledTime).ToListAsync();
    }

    public async Task<List<MedicationIntakeLog>> GetByMedicationIdAsync(Guid medicationId)
    {
        return await _context.MedicationIntakeLogs
            .Include(l => l.ConfirmedBy)
            .Where(l => l.MedicationId == medicationId)
            .OrderByDescending(l => l.ScheduledTime)
            .ToListAsync();
    }
}