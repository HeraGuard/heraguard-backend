using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Medications.Commands;

public class CreateMedicationCommand: IRequest<Result<ReadMedicationDto>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }
    public int Duration { get; set; }
    
    public CreateMedicationCommand(string name, string description, string dosage, string frequency, int duration)
    {
        Name = name;
        Description = description;
        Dosage = dosage;
        Frequency = frequency;
        Duration = duration;
    }
}