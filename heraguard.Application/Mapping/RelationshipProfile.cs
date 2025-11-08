using AutoMapper;
using heraguard.Application.Relationships.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping;

public class RelationshipProfile: Profile
{
    public RelationshipProfile()
    {
        CreateMap<Relationship, ReadRelationshipDto>()
            .ForMember(dest => dest.RelationshipType, 
                opt => opt.MapFrom(src => src.RelationshipTypeId == 2 ? "cuidador" : 
                    src.RelationshipTypeId == 3 ? "doctor" : 
                    "unknown"));
        
        CreateMap<CreateRelationshipDto, Relationship>();
    }
}