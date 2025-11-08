using heraguard.Application.Relationships.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Relationships.Queries;

public class GetRelationshipByIdQuery: IRequest<Result<ReadRelationshipDto>>
{
    public Guid RelationshipId { get; set; }
    public GetRelationshipByIdQuery(Guid relationshipId)
    {
        RelationshipId = relationshipId;
    }
    
}