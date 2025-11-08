using AutoMapper;
using heraguard.Application.Relationships.Command;
using heraguard.Application.Relationships.Dtos;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Relationships.Handlers;

public class DeleteRelationshipHandler:IRequestHandler<DeleteRelationshipCommand, Result<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IRelationshipRepository _repository;

    public DeleteRelationshipHandler(IMapper mapper, IRelationshipRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<Result<Unit>> Handle(DeleteRelationshipCommand request, CancellationToken cancellationToken)
    {
        var relationship = await _repository.GetRelationshipByIdAsync(request.RelationshipId);
        
        if(relationship ==  null)
            return Result<Unit>.Failure(RelationshipErrors.NotFound);
        
        await _repository.DeleteRelationshipAsync(request.RelationshipId);
        
        return Result<Unit>.Success(Unit.Value);
    }
}