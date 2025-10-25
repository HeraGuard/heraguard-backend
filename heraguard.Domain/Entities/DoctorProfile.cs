namespace heraguard.Domain.Entities;

public class DoctorProfile
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public string Especialidad { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string CentroMedico { get; set; } = string.Empty;
    
}