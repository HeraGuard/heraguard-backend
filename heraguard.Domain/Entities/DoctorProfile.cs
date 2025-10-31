namespace heraguard.Domain.Entities;

public class DoctorProfile
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public string Specialty { get; set; } = string.Empty;
    public string MedicalLicense { get; set; } = string.Empty;
    public string MedicalCenter { get; set; } = string.Empty;
    
}