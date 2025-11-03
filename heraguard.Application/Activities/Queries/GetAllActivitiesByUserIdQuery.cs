using heraguard.Application.Activities.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Activities.Queries;

public class GetAllActivitiesByUserIdQuery : IRequest<Result<List<ReadActivityDto>>>
{
    public Guid UserId { get; set; }

    public GetAllActivitiesByUserIdQuery(Guid userid)
    {
        UserId = userid;
    }
}