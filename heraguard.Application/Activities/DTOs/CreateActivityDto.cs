namespace heraguard.Application.Activities.Dtos;

public class CreateActivityDto
{
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeOnly RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid? ElderId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
}