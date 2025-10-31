using heraguard.Application.Activities.Interfaces;
using MediatR;

public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand, Unit>
{
    public readonly IActivityRepository _repository;
    public DeleteActivityHandler(IActivityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _repository.GetActivityByIdAsync(request.Id);
        if (activity == null) throw new KeyNotFoundException($"Actividad con id {request.Id} no encontrada");
        await _repository.DeleteActivityAsync(request.Id);
        return Unit.Value;
    }
}