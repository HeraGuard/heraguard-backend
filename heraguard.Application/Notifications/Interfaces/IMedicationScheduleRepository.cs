using heraguard.Domain.Entities;

namespace heraguard.Application.Notifications.Interfaces;

public interface IMedicationScheduleRepository
{
    Task<MedicationSchedule?> GetByIdAsync(Guid id);
    Task<List<MedicationSchedule>> GetPendingByMedicationAsync(Guid medicationId);
    Task AddAsync(MedicationSchedule schedule);
    Task UpdateAsync(MedicationSchedule schedule);
    Task<int> CountPendingByMedicationAsync(Guid medicationId);
}