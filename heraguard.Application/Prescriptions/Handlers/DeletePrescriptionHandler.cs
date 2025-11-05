using AutoMapper;
using heraguard.Application.Prescriptions.Commands;
using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Prescriptions.Handlers;

public class DeletePrescriptionHandler: IRequestHandler<DeletePrescriptionCommand, Result<Unit>>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;

    public DeletePrescriptionHandler(IPrescriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<Unit>> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _repository.GetPrescriptionByIdAsync(request.PrescriptionId);
        
        if(prescription == null) 
            return Result<Unit>.Failure(PrescriptionErrors.NotFound);
        
        await _repository.DeletePrescriptionAsync(request.PrescriptionId);
        
        return Result<Unit>.Success(Unit.Value);
        
    }
}