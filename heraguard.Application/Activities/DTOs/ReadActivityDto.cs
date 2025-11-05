namespace heraguard.Application.Activities.Dtos;

public class ReadActivityDto
{
    public Guid ActivityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeSpan RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid ElderId { get; set; }
    public Guid? CaregiverId { get; set; }
    public string? DoctorName { get; set; }
    public string? CaregiverName { get; set; }
    public string ElderName { get; set; } = string.Empty;
}