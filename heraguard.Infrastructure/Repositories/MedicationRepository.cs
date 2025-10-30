using heraguard.Application.Medications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;

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

    public Task<Medication> UpdateMedicationAsync(Medication medication)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMedicationAsync(Guid medicationId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Medication>> GetAllMedicationsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Medication> GetMedicationByIdAsync(Guid medicationId)
    {
        throw new NotImplementedException();
    }
}