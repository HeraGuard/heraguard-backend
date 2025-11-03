using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Activities.Commands;

public class DeleteActivityCommand : IRequest<Result<Unit>>
{
    public Guid ActivityId { get; set; }

    public DeleteActivityCommand(Guid activityid)
    {
        ActivityId = activityid;
    }
}