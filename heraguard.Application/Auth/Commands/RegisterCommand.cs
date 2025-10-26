using heraguard.Application.Auth.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Auth.Commands;

public class RegisterCommand : IRequest<Result<AuthResponseDto>>
{
    public RegisterCommand(string email, string password, string name, string lastName, int roleId)
    {
        Email = email;
        Password = password;
        Name = name;
        LastName = lastName;
        RoleId = roleId;
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int RoleId { get; set; }
}