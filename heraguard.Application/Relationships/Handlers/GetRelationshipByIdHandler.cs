using AutoMapper;
using heraguard.Application.Relationships.Dtos;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Application.Relationships.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Relationships.Handlers;

public class GetRelationshipByIdHandler: IRequestHandler<GetRelationshipByIdQuery, Result<ReadRelationshipDto>>
{
    private readonly IMapper _mapper;
    private readonly IRelationshipRepository _repository;

    public GetRelationshipByIdHandler(IMapper mapper, IRelationshipRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<Result<ReadRelationshipDto>> Handle(GetRelationshipByIdQuery request, CancellationToken cancellationToken)
    {
        var relationship = await _repository.GetRelationshipByIdAsync(request.RelationshipId);

        if (relationship == null)
            return Result<ReadRelationshipDto>.Failure(RelationshipErrors.NotFound);

        return Result<ReadRelationshipDto>.Success(_mapper.Map<ReadRelationshipDto>(relationship));
    }
}