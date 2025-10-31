using heraguard.Application.Medications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class MedicationRepository : IMedicationRepository
{
    private readonly HeraGuardDbContext  _context;

    public MedicationRepository(HeraGuardDbContext context)
    {
        _context = context;
    }
    public async Task<Medication> AddMedicationAsync(Medication medication)
    {
        await _context.Medications.AddAsync(medication);
        await _context.SaveChangesAsync();
        return medication;
    }

    public async Task<Medication> UpdateMedicationAsync(Medication medication)
    {
        _context.Medications.Update(medication);
        await _context.SaveChangesAsync();
        return medication;
    }

    public async Task<bool> DeleteMedicationAsync(Guid medicationId)
    {
        var deleted = await _context.Medications
            .Where(m => m.MedicationId == medicationId)
            .ExecuteDeleteAsync();
    
        return deleted > 0;
    }

    public async Task<List<Medication>> GetAllMedicationsByUserIdAsync(Guid userId)
    {
        return await _context.Medications
            .Where(m => m.ElderId == userId)
            .Include(m => m.DoctorProfile)
            .ThenInclude(d => d.User)
            .Include(m => m.CaregiverProfile)
            .ThenInclude(c => c.User)
            .Include(m => m.ElderProfile)
            .ThenInclude(e => e.User)
            .ToListAsync();
    }

    public async Task<Medication?> GetMedicationByIdAsync(Guid medicationId)
    {
        return await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == medicationId);
    }
    
    // MedicationRepository.cs
    public async Task<Medication> GetMedicationByIdWithRelationsAsync(Guid medicationId)
    {
        return await _context.Medications
            .Include(m => m.DoctorProfile)
            .ThenInclude(d => d.User)
            .Include(m => m.CaregiverProfile)
            .ThenInclude(c => c.User)
            .Include(m => m.ElderProfile)
            .ThenInclude(e => e.User)
            .FirstOrDefaultAsync(m => m.MedicationId == medicationId);
    }

}