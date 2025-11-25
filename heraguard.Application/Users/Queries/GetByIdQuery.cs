using heraguard.Application.Users.DTOs;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Users.Queries;

public class GetByIdQuery : IRequest<Result<UserDto>>
{
    public Guid UserId { get; set; }

    public GetByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}