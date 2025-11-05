using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly HeraGuardDbContext  _context;

    public PrescriptionRepository(HeraGuardDbContext context)
    {
        _context = context;
    }
    public async Task<Prescription> addPrescriptionAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
        return prescription;
    }

    public Task<bool> DeletePrescriptionAsync(Guid prescriptionId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Prescription>> GetAllPrescriptionsByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Prescription?> GetPrescriptionByIdAsync(Guid prescriptionId)
    {
        return await _context.Prescriptions
            .Include(p => p.Medications)
            .Include(p => p.Elder)
            .ThenInclude(e => e.User) 
            .Include(p => p.Doctor)
            .ThenInclude(d => d.User) 
            .FirstOrDefaultAsync(p => p.Id == prescriptionId);

    }

}