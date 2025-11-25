namespace heraguard.Application.Notifications.Dtos;

public class MedicationIntakeDto
{
    public Guid ScheduleId { get; set; }
    public Guid MedicationId { get; set; }
    public Guid ElderId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public string Status { get; set; }
    public string MedicationName { get; set; }
    public string Dosage { get; set; }
}
