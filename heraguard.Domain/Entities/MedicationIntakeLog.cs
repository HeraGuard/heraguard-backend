namespace heraguard.Domain.Entities;

public class MedicationIntakeLog
{
    public Guid Id { get; set; }
    public Guid MedicationScheduleId { get; set; }
    public Guid MedicationId { get; set; }
    public Guid ElderId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualTime { get; set; } // Hora real que lo tomó
    public string Status { get; set; } = string.Empty; // taken/missed
    public Guid? ConfirmedByUserId { get; set; } // Elder o Cuidador que marcó
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public MedicationSchedule Schedule { get; set; } = null!;
    public Medication Medication { get; set; } = null!;
    public ElderProfile Elder { get; set; } = null!;
    public User? ConfirmedBy { get; set; }
}