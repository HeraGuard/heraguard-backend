using System.Data.Common;
using heraguard.Application.Users.DTOs;

public class ActivityReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeOnly RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid DoctorId { get; set; }
    public Guid AdultoId { get; set; }
}