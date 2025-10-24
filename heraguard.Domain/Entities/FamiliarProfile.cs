namespace heraguard.Domain.Entities;

public class FamiliarProfile
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Parentesco { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
}