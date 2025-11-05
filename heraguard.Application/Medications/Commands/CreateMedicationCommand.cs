using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Medications.Commands;

public class CreateMedicationCommand: IRequest<Result<ReadMedicationDto>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Dosage { get; set; }
    public int Frequency { get; set; }
    public int Duration { get; set; }
    
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid ElderId { get; set; }
    
    public CreateMedicationCommand(string name, string description, string dosage, int frequency, int duration,  Guid? doctorId, Guid? caregiverId, Guid elderId)
    {
        Name = name;
        Description = description;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
        DoctorId = doctorId;
        CaregiverId = caregiverId;
        ElderId = elderId;
    }
}