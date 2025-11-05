using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Prescriptions.Dtos;

public class CreatePrescriptionDto
{
    public Guid ElderId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public List<CreateMedicationDto> Medications { get; set; } = new List<CreateMedicationDto>();
}