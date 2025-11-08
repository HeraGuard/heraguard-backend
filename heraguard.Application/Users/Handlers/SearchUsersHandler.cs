using AutoMapper;
using heraguard.Application.Users.DTOs;
using heraguard.Application.Users.Interfaces;
using heraguard.Application.Users.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Users.Handlers;

public class SearchUsersHandler : IRequestHandler<SearchUsersQuery, Result<List<UserDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public SearchUsersHandler(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<Result<List<UserDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.SearchUsersAsync(request.Query, request.RoleId);

        var userDtos = _mapper.Map<List<UserDto>>(users);
        return Result<List<UserDto>>.Success(userDtos);
    }
}