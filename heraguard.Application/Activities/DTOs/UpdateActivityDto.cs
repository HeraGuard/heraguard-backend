namespace heraguard.Application.Activities.Dtos;

public class UpdateActivityDto
{
    public string? Name { get; set; }
    public string? Frequency { get; set; }
    public TimeSpan? RecommendedTime { get; set; }
    public string? Duration { get; set; }
    public string? Notes { get; set; }
    public Guid? ElderId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? Caregiver { get; set; }
}