namespace heraguard.Application.Users.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    // Elder
    public string? LinkingCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? EmergencyContact { get; set; }
    public string? Address { get; set; }

    // Doctor  
    public string? Specialty { get; set; }
    public string? MedicalLicense { get; set; }
    public string? MedicalCenter { get; set; }

    // Caregiver
    public string? Relationship { get; set; }
    public string? PhoneNumber { get; set; }
}