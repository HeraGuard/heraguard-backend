namespace heraguard.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } =  string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public string? RefreshToken { get; set; } 
    public DateTime? RefreshTokenExpires { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public ElderProfile? AdultoMayorProfile { get; set; }
    public CaregiverProfile? FamiliarProfile { get; set; }
    public DoctorProfile? DoctorProfile { get; set; }
}