using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class UserDeviceTokenRepository : IUserDeviceTokenRepository
{
    private readonly HeraGuardDbContext _context;

    public UserDeviceTokenRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetDeviceTokensByUserIdAsync(Guid userId)
    {
        return await _context.UserDeviceTokens
            .Where(t => t.UserId == userId)
            .Select(t => t.DeviceToken)
            .ToListAsync();
    }

    public async Task AddOrUpdateAsync(UserDeviceToken token)
    {
        var existing = await _context.UserDeviceTokens
            .FirstOrDefaultAsync(t => t.UserId == token.UserId);

        if (existing != null)
        {
            existing.DeviceToken = token.DeviceToken;
            existing.Platform = token.Platform;
            existing.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            await _context.UserDeviceTokens.AddAsync(token);
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task RemoveInvalidTokenAsync(string deviceToken)
    {
        var token = await _context.UserDeviceTokens
            .FirstOrDefaultAsync(t => t.DeviceToken == deviceToken);

        if (token != null)
        {
            _context.UserDeviceTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UserDeviceToken?> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserDeviceTokens
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }
}