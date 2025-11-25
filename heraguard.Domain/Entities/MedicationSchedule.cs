namespace heraguard.Domain.Entities;

public class MedicationSchedule
{
    public Guid Id { get; set; }
    public Guid MedicationId { get; set; }
    public Guid ElderId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public bool NotificationSent { get; set; }
    public bool CriticalAlertSent { get; set; } // La alarma fuerte de 15 min después
    public string Status { get; set; } = "pending"; // pending/confirmed/missed
    public DateTime CreatedAt { get; set; }
    
    public Medication Medication { get; set; }
    public ElderProfile Elder { get; set; }
}