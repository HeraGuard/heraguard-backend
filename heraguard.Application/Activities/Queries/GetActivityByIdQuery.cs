using MediatR;

public class GetActivitByIdyQuery : IRequest<ActivityCreateDto>
{
    public Guid Id { get; }

    public GetActivitByIdyQuery(Guid id)
    {
        Id = id;
    }
}