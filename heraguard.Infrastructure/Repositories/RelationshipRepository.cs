using heraguard.Application.Relationships.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Repositories;

public class RelationshipRepository : IRelationshipRepository
{
    private readonly HeraGuardDbContext _context;

    public RelationshipRepository(HeraGuardDbContext context)
    {
        _context = context;
    }
    public async Task<Relationship> AddRelationshipAsync(Relationship relationship)
    {
        await _context.Relationships.AddAsync(relationship);
        await _context.SaveChangesAsync();
        return await _context.Relationships
            .Include(r => r.Elder)
            .Include(r => r.RelatedUser)
            .FirstAsync(r => r.RelationshipId == relationship.RelationshipId);
    }

    public async Task<bool> DeleteRelationshipAsync(Guid relationshipId)
    {
        var deleted = await _context.Relationships
            .Where(r => r.RelationshipId == relationshipId)
            .ExecuteDeleteAsync();
    
        return deleted > 0;
    }

    public async Task<Relationship?> GetRelationshipByIdAsync(Guid relationshipId)
    {
        return await _context.Relationships
            .Include(r => r.Elder)
            .Include(r => r.RelatedUser)
            .FirstOrDefaultAsync(r => r.RelationshipId == relationshipId);
    }

    public async Task<List<Relationship>> GetRelationshipsByUserId(Guid userId, int typeId)
    {
        return await _context.Relationships
            .Where(r => r.RelatedUserId == userId || r.ElderId == userId)
            .Where(r => r.RelationshipTypeId == typeId)
            .ToListAsync();
    }

    public async Task<Relationship?> GetByIdWithIncludesAsync(Guid relationshipId)
    {
        return await _context.Relationships
            .Include(r => r.Elder)
            .Include(r => r.RelatedUser)
            .FirstOrDefaultAsync(r => r.RelationshipId == relationshipId);
    }

}