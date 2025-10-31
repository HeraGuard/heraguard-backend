namespace heraguard.Domain.Entities;

public class Activity
{
    public Guid ActivityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeSpan RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid ElderId { get; set; }
    public DoctorProfile? DoctorProfile { get; set; }
    public CaregiverProfile? CaregiverProfile { get; set; }
    public ElderProfile ElderProfile { get; set; } 
}