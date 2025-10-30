using heraguard.Application.Auth.Dtos;
using heraguard.Domain.Common;

namespace heraguard.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(string email, string password);

    Task<Result<AuthResponseDto>>
        RegisterAsync(string email, string password, string name, string lastName, int roleId);
    
    Task<Result> LogoutAsync();
    
    Task<Result<AuthResponseDto>> RefreshTokenAsync(string refreshToken);
}