using heraguard.Application.Users.DTOs;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Users.Queries;

public class SearchUsersQuery : IRequest<Result<List<UserDto>>>
{
    public string Query { get; set; }
    public int RoleId { get; set; }
    
    public SearchUsersQuery(string query, int roleId)
    {
        Query = query;
        RoleId = roleId;
    }
}