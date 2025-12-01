namespace heraguard.Domain.Entities;

public class SosEvent
{
    public Guid Id { get; set; }
    public Guid ElderId { get; set; }
    public User Elder { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    
    public string? Status { get; set; } 
    public string? Notes { get; set; }
}
