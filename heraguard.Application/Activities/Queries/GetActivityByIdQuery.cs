using heraguard.Application.Activities.Dtos;
using MediatR;

public class GetActivitByIdyQuery : IRequest<CreateActivityDto>
{
    public Guid Id { get; }

    public GetActivitByIdyQuery(Guid id)
    {
        Id = id;
    }
}