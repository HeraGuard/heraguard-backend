using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Medications.Commands;

public class UpdateMedicationCommand : IRequest<Result<ReadMedicationDto>>
{
    public Guid MedicationId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public int? Duration { get; set; }
    public DateTime? StartDate { get; set; }
    
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid? ElderId { get; set; }

    public UpdateMedicationCommand(Guid medicationId, string? name, string? description, string? dosage, string frequency, int? duration, DateTime startDate, Guid? doctorId, Guid? caregiverId, Guid? elderId)
    {
        MedicationId = medicationId;
        Name = name;
        Description = description;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        DoctorId = doctorId;
        CaregiverId = caregiverId;
        ElderId = elderId;
        StartDate = startDate;
    }
}