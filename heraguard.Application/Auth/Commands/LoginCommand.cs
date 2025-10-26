using heraguard.Application.Auth.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Auth.Commands;

public class LoginCommand : IRequest<Result<AuthResponseDto>>
{
    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}