using heraguard.Domain.Entities;

namespace heraguard.Application.Prescriptions.Interfaces;

public interface IPrescriptionRepository
{
    Task<Prescription> addPrescriptionAsync(Prescription prescription);
    Task<bool> DeletePrescriptionAsync(Guid prescriptionId);
    Task<List<Prescription>> GetAllPrescriptionsByUserIdAsync(Guid userId);
    Task<Prescription?> GetPrescriptionByIdAsync(Guid prescriptionId);
}