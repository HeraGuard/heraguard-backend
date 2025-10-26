using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Dtos;
using heraguard.Application.Auth.Interfaces;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IAuthService _authService;

    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.Email, request.Password);
    }
}