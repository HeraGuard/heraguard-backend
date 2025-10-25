namespace heraguard.Domain.Entities;

public class AdultoMayorProfile
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public DateTime FechaNacimiento { get; set; }
    public string ContactoEmergencia { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
}