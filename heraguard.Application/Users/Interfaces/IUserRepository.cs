using heraguard.Domain.Entities;

namespace heraguard.Application.Users.Interfaces;

public interface IUserRepository
{
    Task<User> GetByLinkingCodeAsync(string linkingCode);
    Task<List<User>> SearchUsersAsync(string query, int roleId);
}