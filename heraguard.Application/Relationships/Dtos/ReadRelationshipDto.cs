using heraguard.Application.Users.DTOs;

namespace heraguard.Application.Relationships.Dtos;

public class ReadRelationshipDto
{
    public Guid RelationshipId { get; set; }
    public UserDto Elder { get; set; } = new UserDto();
    public UserDto RelatedUser { get; set; } = new UserDto();
    public String RelationshipType { get; set; } = string.Empty;
    
}