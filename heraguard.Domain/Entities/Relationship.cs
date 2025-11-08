namespace heraguard.Domain.Entities;

public class Relationship
{
    public Guid RelationshipId { get; set; }
    public Guid ElderId { get; set; }
    public Guid RelatedUserId { get; set; }
    public int RelationshipTypeId { get; set; }

    public User Elder { get; set; } 
    public User RelatedUser { get; set; } 
}