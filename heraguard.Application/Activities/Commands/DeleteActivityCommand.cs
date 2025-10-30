using MediatR;

public class DeleteActivityCommand : IRequest<Unit>
{
    public Guid Id { get; }

    public DeleteActivityCommand(Guid id)
    {
        Id = id;
    }
}