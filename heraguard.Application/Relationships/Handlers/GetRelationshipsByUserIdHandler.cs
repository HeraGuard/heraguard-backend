using AutoMapper;
using heraguard.Application.Relationships.Dtos;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Application.Relationships.Queries;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Relationships.Handlers;

public class GetRelationshipsByUserIdHandler:IRequestHandler<GetRelationshipsByUserIdQuery, Result<List<ReadRelationshipDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRelationshipRepository _repository;

    public GetRelationshipsByUserIdHandler(IMapper mapper, IRelationshipRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<Result<List<ReadRelationshipDto>>> Handle(GetRelationshipsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var relationships = await _repository.GetRelationshipsByUserId(request.UserId, request.TypeId);
        
        return Result<List<ReadRelationshipDto>>.Success(_mapper.Map<List<ReadRelationshipDto>>(relationships));
    }
}