namespace heraguard.Application.Relationships.Dtos;

public class CreateRelationshipDto
{
    public Guid ElderId { get; set; }
    public Guid RelatedUserId { get; set; }
    public int RelationshipTypeId { get; set; }
}