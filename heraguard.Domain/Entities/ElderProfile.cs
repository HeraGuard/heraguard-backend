namespace heraguard.Domain.Entities;

public class ElderProfile
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string EmergencyContact { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LinkingCode { get; private set; } = string.Empty;

    public void GenerateLinkingCode()
    {
        LinkingCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
    }
}