using heraguard.Application.Sos.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;

namespace heraguard.Infrastructure.Repositories;

public class SosEventRepository: ISosEventRepository
{
    private readonly HeraGuardDbContext _context;

    public SosEventRepository(HeraGuardDbContext context)
    {
        _context = context;
    }

    public async Task<SosEvent> CreateAsync(SosEvent sosEvent)
    {
        await _context.SosEvents.AddAsync(sosEvent);
        await _context.SaveChangesAsync();
        return sosEvent;
    }
}