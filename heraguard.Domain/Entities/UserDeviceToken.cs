namespace heraguard.Domain.Entities;

public class UserDeviceToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DeviceToken { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty; // "android" o "ios"
    public DateTime UpdatedAt { get; set; }
    
    public User User { get; set; }
}