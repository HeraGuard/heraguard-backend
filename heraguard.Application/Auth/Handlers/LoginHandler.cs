using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Dtos;
using heraguard.Application.Auth.Interfaces;
using MediatR;

namespace heraguard.Application.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IAuthService _authService;

    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (result == null)
            throw new UnauthorizedAccessException("Credenciales inválidas");

        return result;
    }
}