using AutoMapper;
using heraguard.Application.Medications.Commands;
using heraguard.Application.Medications.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Medications.Handlers;

public class DeleteMedicationHandler : IRequestHandler<DeleteMedicationCommand, Result<Unit>>
{
    private readonly IMedicationRepository _repository;

    public DeleteMedicationHandler(IMedicationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<Unit>> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
    {
        var medication = await _repository.GetMedicationByIdAsync(request.MedicationId);
        
        if(medication == null)
            return Result<Unit>.Failure(MedicationErrors.NotFound);
        
        await _repository.DeleteMedicationAsync(request.MedicationId);
        return Result<Unit>.Success(Unit.Value);
    }
}