using AutoMapper;
using heraguard.Application.Auth.Interfaces;
using heraguard.Application.Medications.Commands;
using heraguard.Application.Medications.Dtos;
using heraguard.Application.Medications.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Medications.Handlers;

public class CreateMedicationHandler: IRequestHandler<CreateMedicationCommand, Result<ReadMedicationDto>>
{
    private readonly IMedicationRepository _repository;
    private readonly IMapper _mapper;

    public CreateMedicationHandler(IMedicationRepository repository,  IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<ReadMedicationDto>> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
    {
        var medication = _mapper.Map<Medication>(request);  
        medication.MedicationId = Guid.NewGuid();
    
        var result = await _repository.AddMedicationAsync(medication);
        
        var medicationDto = _mapper.Map<ReadMedicationDto>(result);
    
        return Result<ReadMedicationDto>.Success(medicationDto); 
    }

}