using heraguard.Domain.Entities;

namespace heraguard.Application.Notifications.Interfaces;

public interface IMedicationIntakeLogRepository
{
    Task AddAsync(MedicationIntakeLog log);
    Task<List<MedicationIntakeLog>> GetByElderIdAsync(Guid elderId, DateTime? startDate, DateTime? endDate);
    Task<List<MedicationIntakeLog>> GetByMedicationIdAsync(Guid medicationId);
}