using heraguard.Domain.Entities;

namespace heraguard.Application.Relationships.Interfaces;

public interface IRelationshipRepository
{
    Task<Relationship> AddRelationshipAsync(Relationship relationship);
    Task<bool> DeleteRelationshipAsync(Guid relationshipId);
    Task<Relationship?> GetRelationshipByIdAsync(Guid relationshipId);
    Task<List<Relationship>> GetRelationshipsByUserId(Guid userId, int typeId);
    Task<Relationship?> GetByIdWithIncludesAsync(Guid relationshipId);

}