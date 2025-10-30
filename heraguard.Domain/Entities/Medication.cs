namespace heraguard.Domain.Entities;

public class Medication
{
    public Guid MedicationId { get; set; }
    public string Name { get; set; } =  string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public int Duration { get; set; } 
    
}