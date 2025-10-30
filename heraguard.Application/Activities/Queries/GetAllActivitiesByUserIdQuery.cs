using MediatR;

public class GetAllActivitiesByUserIdQuery : IRequest<IEnumerable<ActivityCreateDto>>
{
    public Guid Id { get; }

    public GetAllActivitiesByUserIdQuery(Guid id)
    {
        Id = id;
    }
}