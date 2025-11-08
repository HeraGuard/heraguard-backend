using AutoMapper;
using heraguard.Application.Users.DTOs;
using heraguard.Application.Users.Interfaces;
using heraguard.Application.Users.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Users.Handlers;

public class GetUserByLinkingCodeHandler :IRequestHandler<GetUserByLinkingCodeQuery, Result<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public GetUserByLinkingCodeHandler(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<Result<UserDto>> Handle(GetUserByLinkingCodeQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByLinkingCodeAsync(request.LinkingCode);
        
        if(user ==  null)
            return Result<UserDto>.Failure(UserErrors.NotFound);
        
        return Result<UserDto>.Success(_mapper.Map<UserDto>(user));

    }
}