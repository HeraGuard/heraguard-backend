using heraguard.Application.Relationships.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Relationships.Command;

public class CreateRelationshipCommand: IRequest<Result<ReadRelationshipDto>>
{
    public string LinkingCode { get; set; }
    public Guid RelatedUserId { get; set; }
    public int RelationshipTypeId { get; set; }

    public CreateRelationshipCommand(string linkingCode, Guid relatedUserId, int relationshipTypeId)
    {
        LinkingCode = linkingCode;
        RelatedUserId = relatedUserId;
        RelationshipTypeId = relationshipTypeId;
    }
}