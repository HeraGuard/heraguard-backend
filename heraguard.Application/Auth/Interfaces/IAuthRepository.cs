using heraguard.Domain.Entities;

namespace heraguard.Application.Auth.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
}