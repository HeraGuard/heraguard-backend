using heraguard.Application.Auth.Dtos;
using MediatR;

namespace heraguard.Application.Auth.Commands;

public class RegisterCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int RoleId { get; set; }

    public RegisterCommand(string email, string password, string name, string lastName, int roleId)
    {
        Email = email;
        Password = password;
        Name = name;
        LastName = lastName;
        RoleId = roleId;
    }
}