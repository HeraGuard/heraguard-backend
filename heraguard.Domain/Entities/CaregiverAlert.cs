using System.ComponentModel.DataAnnotations.Schema;

namespace heraguard.Domain.Entities;

public class CaregiverAlert
{
    public Guid Id { get; set; }
    [ForeignKey("Caregiver")]
    public Guid CaregiverId { get; set; }
    public Guid ElderId { get; set; }
    public Guid? MedicationId { get; set; }
    public string AlertType { get; set; } = string.Empty; // missed_medication/multiple_missed
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public CaregiverProfile Caregiver { get; set; } = null!;
    public ElderProfile Elder { get; set; } = null!;
    public Medication? Medication { get; set; }
}