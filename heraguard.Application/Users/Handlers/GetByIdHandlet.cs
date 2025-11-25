using AutoMapper;
using heraguard.Application.Users.DTOs;
using heraguard.Application.Users.Interfaces;
using heraguard.Application.Users.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Users.Handlers;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<UserDto>>
{
    private readonly IMapper _mapper;
    public readonly IUserRepository _repository;

    public GetByIdHandler(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<UserDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId);
        if (user == null) return Result<UserDto>.Failure(UserErrors.NotFound);
        var userDto = _mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(userDto);
    }
}