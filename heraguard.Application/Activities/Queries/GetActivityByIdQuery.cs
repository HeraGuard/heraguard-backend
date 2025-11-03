using heraguard.Application.Activities.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Activities.Queries;

public class GetActivitByIdyQuery : IRequest<Result<ReadActivityDto>>
{
    public Guid ActivityId { get; set; }

    public GetActivitByIdyQuery(Guid activityId)
    {
        ActivityId = activityId;
    }
}