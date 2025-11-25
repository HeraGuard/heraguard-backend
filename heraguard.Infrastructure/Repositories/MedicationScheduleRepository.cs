using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class MedicationScheduleRepository : IMedicationScheduleRepository
{
    private readonly HeraGuardDbContext _context;

    public MedicationScheduleRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task<MedicationSchedule?> GetByIdAsync(Guid id)
    {
        return await _context.MedicationSchedules
            .Include(s => s.Medication)
            .Include(s => s.Elder)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<MedicationSchedule>> GetPendingByMedicationAsync(Guid medicationId)
    {
        return await _context.MedicationSchedules
            .Where(s => s.MedicationId == medicationId && s.Status == "pending")
            .OrderBy(s => s.ScheduledTime)
            .ToListAsync();
    }

    public async Task AddAsync(MedicationSchedule schedule)
    {
        await _context.MedicationSchedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MedicationSchedule schedule)
    {
        _context.MedicationSchedules.Update(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountPendingByMedicationAsync(Guid medicationId)
    {
        return await _context.MedicationSchedules
            .CountAsync(s => s.MedicationId == medicationId && s.Status == "pending");
    }
}