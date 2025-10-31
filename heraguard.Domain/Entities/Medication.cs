namespace heraguard.Domain.Entities;

public class Medication
{
    public Guid MedicationId { get; set; }
    public string Name { get; set; } =  string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public int Duration { get; set; } 
    
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid ElderId { get; set; }
    
    public DoctorProfile? DoctorProfile { get; set; }
    public CaregiverProfile? CaregiverProfile { get; set; }
    public ElderProfile ElderProfile { get; set; } 
    
}