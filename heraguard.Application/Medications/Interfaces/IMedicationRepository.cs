using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;

namespace heraguard.Application.Medications.Interfaces;

public interface IMedicationRepository
{
    Task<Medication> AddMedicationAsync(Medication medication);
    Task<Medication> UpdateMedicationAsync(Medication medication);
    Task<bool> DeleteMedicationAsync(Guid medicationId);
    Task<List<Medication>> GetAllMedicationsAsync();
    Task<Medication> GetMedicationByIdAsync(Guid medicationId);


}