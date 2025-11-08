using heraguard.Application.Relationships.Dtos;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Relationships.Queries;

public class GetRelationshipsByUserIdQuery: IRequest<Result<List<ReadRelationshipDto>>>
{
    public Guid UserId { get; set; }
    public int TypeId { get; set; }

    public GetRelationshipsByUserIdQuery(Guid userId, int typeId)
    {
        UserId = userId;
        TypeId = typeId;
    }
}