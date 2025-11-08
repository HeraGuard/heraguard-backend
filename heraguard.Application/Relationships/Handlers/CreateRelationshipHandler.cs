using AutoMapper;
using heraguard.Application.Relationships.Command;
using heraguard.Application.Relationships.Dtos;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Application.Users.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Relationships.Handlers;

public class CreateRelationshipHandler : IRequestHandler<CreateRelationshipCommand, Result<ReadRelationshipDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRelationshipRepository _relationshipRepository;
    private readonly IMapper _mapper;

    public CreateRelationshipHandler(IUserRepository userRepository, IRelationshipRepository relationshipRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _relationshipRepository = relationshipRepository;
        _mapper = mapper;
    }
    public async Task<Result<ReadRelationshipDto>> Handle(CreateRelationshipCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByLinkingCodeAsync(request.LinkingCode);
        
        if (user == null)
        {
            return Result<ReadRelationshipDto>.Failure(UserErrors.InvalidCode);
        }

        var createRelationshipDto = new CreateRelationshipDto
        {
            ElderId = user.Id,
            RelatedUserId = request.RelatedUserId,
            RelationshipTypeId = request.RelationshipTypeId
        };
        
        var relationship = _mapper.Map<Relationship>(createRelationshipDto);
        relationship.RelationshipId = Guid.NewGuid();
        
        var result = await _relationshipRepository.AddRelationshipAsync(relationship);
        
        var savedRelationship = await _relationshipRepository.GetByIdWithIncludesAsync(relationship.RelationshipId);

        
        var relationshipDto = _mapper.Map<ReadRelationshipDto>(result);
        return Result<ReadRelationshipDto>.Success(relationshipDto);
        
    }
}