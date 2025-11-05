using heraguard.Application.Activities.Commands;
using heraguard.Application.Activities.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand, Result<Unit>>
{
    public readonly IActivityRepository _repository;
    public DeleteActivityHandler(IActivityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _repository.GetActivityByIdAsync(request.ActivityId);
        if (activity == null) return Result<Unit>.Failure(ActivityErrors.NotFound);
        await _repository.DeleteActivityAsync(request.ActivityId);
        return Result<Unit>.Success(Unit.Value);
    }
}