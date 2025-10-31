namespace heraguard.Application.Activities.Dtos;

public class ReadActivityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeSpan RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
}