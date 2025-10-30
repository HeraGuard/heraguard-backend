using heraguard.Application.Users.DTOs;

public class ActivityUpdateDto
{
    public string? Name { get; set; }
    public string? Frequency { get; set; }
    public TimeOnly? RecommendedTime { get; set; }
    public string? Duration { get; set; }
    public string? Notes { get; set; }
    public Guid AdultoId { get; set; }
}