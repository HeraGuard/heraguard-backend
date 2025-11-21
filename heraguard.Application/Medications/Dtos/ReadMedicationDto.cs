namespace heraguard.Application.Medications.Dtos;

public class ReadMedicationDto
{
    public Guid MedicationId { get; set; }
    public string Name { get; set; } =  string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public int Frequency { get; set; } 
    public int Duration { get; set; } 
    public DateTime StartDate { get; set; }
    
    public Guid? DoctorId { get; set; }
    public Guid ElderId { get; set; }
    public Guid? CaregiverId { get; set; }
    
    public string? DoctorName { get; set; }
    public string? CaregiverName { get; set; }
    public string ElderName { get; set; } = string.Empty;
}