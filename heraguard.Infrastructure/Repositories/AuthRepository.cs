using heraguard.Application.Auth.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly HeraGuardDbContext _context;

    public AuthRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task CreateElderProfileAsync(Guid userId)
    {
        var elderProfile = new ElderProfile { UserId = userId };
        elderProfile.GenerateLinkingCode();
        await _context.Elders.AddAsync(elderProfile);
        await _context.SaveChangesAsync();
    }

    public async Task CreateDoctorProfileAsync(Guid userId)
    {
        var doctorProfile = new DoctorProfile { UserId = userId };
        await _context.Doctors.AddAsync(doctorProfile);
        await _context.SaveChangesAsync();
    }

    public async Task CreateCaregiverProfileAsync(Guid userId)
    {
        var caregiverProfile = new CaregiverProfile { UserId = userId };
        await _context.Caregivers.AddAsync(caregiverProfile);
        await _context.SaveChangesAsync();
    }

    public async Task<ElderProfile?> GetElderProfileByUserIdAsync(Guid userId)
    {
        return await _context.Elders
            .FirstOrDefaultAsync(e => e.UserId == userId);
    }
}