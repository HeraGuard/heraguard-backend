using heraguard.Application.Auth.Dtos;

namespace heraguard.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(string email, string password);
    Task<AuthResponseDto> RegisterAsync(string email, string password, string name, string lastName, int roleId);
}