using heraguard.Application.Activities.Dtos;
using MediatR;

public class GetAllActivitiesByUserIdQuery : IRequest<IEnumerable<CreateActivityDto>>
{
    public Guid Id { get; }

    public GetAllActivitiesByUserIdQuery(Guid id)
    {
        Id = id;
    }
}