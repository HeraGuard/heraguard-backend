namespace heraguard.Domain.Entities;

public class CaregiverProfile
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public string Relationship { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}