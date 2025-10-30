public class Activity
{
    public Guid ActivityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeOnly RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid DoctorID { get; set; }
    public Guid AdultoId { get; set; }
}