using heraguard.Application.Users.DTOs;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Users.Queries;

public class GetUserByLinkingCodeQuery :IRequest<Result<UserDto>>
{
    public string LinkingCode { get; set; } = string.Empty;

    public GetUserByLinkingCodeQuery(string linkingCode)
    {
        LinkingCode = linkingCode;
    }
}