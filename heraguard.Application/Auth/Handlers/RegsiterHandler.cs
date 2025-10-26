using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Dtos;
using heraguard.Application.Auth.Interfaces;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Auth.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
{
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(
            request.Email,
            request.Password,
            request.Name,
            request.LastName,
            request.RoleId
        );
    }
}