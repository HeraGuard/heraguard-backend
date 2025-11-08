using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Relationships.Command;

public class DeleteRelationshipCommand:IRequest<Result<Unit>>
{
    public Guid RelationshipId { get; set; }
    
    public DeleteRelationshipCommand(Guid relationshipId)
    {
        RelationshipId = relationshipId;
    }
}