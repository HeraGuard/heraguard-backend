namespace heraguard.Application.Medications.Dtos;

public class UpdateMedicationDto
{
    public Guid MedicationId { get; set; }
    public string Name { get; set; } =  string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public int Frequency { get; set; } 
    public int Duration { get; set; } 
    
    public Guid? DoctorId { get; set; }
    public Guid? ElderId { get; set; }
    public Guid? CaregiverId { get; set; }
}