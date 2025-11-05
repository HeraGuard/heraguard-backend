using heraguard.Domain.Entities;

namespace heraguard.Application.MedicalAppointments.Interfaces;

public interface IMedicalAppointmentRepository
{
    Task<MedicalAppointment> AddMedicalAppointmentAsync(MedicalAppointment medicalAppointment);
    Task<MedicalAppointment> UpdateMedicalAppointmentAsync(MedicalAppointment medicalAppointment);
    Task<bool> DeleteMedicalAppointmentAsync(MedicalAppointment medicalAppointment);
    Task<List<MedicalAppointment>> GetAllMedicalAppointmentByUserIdAsync(Guid userId);
    Task<MedicalAppointment?> GetMedicalAppointmentByIdAsync(Guid medicalAppointmentId);
    Task<MedicalAppointment?> GetMedicalAppointmentByIdWithRelationAsync(Guid medicalAppointmentId);
}
