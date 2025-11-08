using AutoMapper;
using heraguard.Application.Users.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly HeraGuardDbContext _context;

    public UserRepository(HeraGuardDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetByLinkingCodeAsync(string linkingCode)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.AdultoMayorProfile.LinkingCode == linkingCode);
    }
}