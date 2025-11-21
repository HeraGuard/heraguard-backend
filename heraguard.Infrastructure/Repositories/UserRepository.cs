using AutoMapper;
using heraguard.Application.Users.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HeraGuardDbContext _context;

    public UserRepository(HeraGuardDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetByLinkingCodeAsync(string linkingCode)
    {
        return await _context.Users
            .Include(u => u.AdultoMayorProfile)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.AdultoMayorProfile.LinkingCode == linkingCode);
    }


    public async Task<List<User>> SearchUsersAsync(string query, int roleId)
    {
        var searchTerm = query?.Trim().ToLower() ?? string.Empty;

        var usersQuery = _context.Users
            .Include(u => u.Role)
            .AsNoTracking()
            .Where(u => u.RoleId == roleId);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            usersQuery = usersQuery.Where(u =>
                u.Name.ToLower().Contains(searchTerm) ||
                u.LastName.ToLower().Contains(searchTerm));
        }

        return await usersQuery
            .OrderBy(u => u.Name)
            .Take(10)
            .ToListAsync();

    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Include(u => u.AdultoMayorProfile)
            .Include(u => u.DoctorProfile)
            .Include(u => u.FamiliarProfile)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}