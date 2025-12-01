namespace heraguard.Application.Sos.Dtos;

public class SosEventDto
{
    public Guid Id { get; set; }
    public Guid ElderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Status { get; set; }
}