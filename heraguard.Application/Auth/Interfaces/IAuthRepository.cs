using heraguard.Domain.Entities;

namespace heraguard.Application.Auth.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task CreateElderProfileAsync(Guid userId);
    Task CreateDoctorProfileAsync(Guid userId);
    Task CreateCaregiverProfileAsync(Guid userId);
}