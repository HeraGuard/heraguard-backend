namespace heraguard.Domain.Entities;

public class ElderProfile
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string EmergencyContact { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}