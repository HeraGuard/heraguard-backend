using AutoMapper;
using heraguard.Application.Medications.Commands;
using heraguard.Application.Medications.Dtos;
using heraguard.Application.Medications.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Medications.Handlers;

public class UpdateMedicationHandler: IRequestHandler<UpdateMedicationCommand, Result<ReadMedicationDto>>
{
    private readonly IMedicationRepository _repository;
    private readonly IMapper _mapper;

    public UpdateMedicationHandler(IMedicationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<ReadMedicationDto>> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
    {
        var medication = await _repository.GetMedicationByIdAsync(request.MedicationId);
        
        if (medication == null)
            return Result<ReadMedicationDto>.Failure(MedicationErrors.NotFound);
        
        if (request.Name != null) medication.Name = request.Name;
        if (request.Description != null) medication.Description = request.Description;
        if (request.Dosage != null) medication.Dosage = request.Dosage;
        if (request.Frequency != null) medication.Frequency = request.Frequency;
        if (request.Duration.HasValue) medication.Duration = request.Duration.Value;
        if (request.DoctorId.HasValue) medication.DoctorId = request.DoctorId;
        if (request.CaregiverId.HasValue) medication.CaregiverId = request.CaregiverId;
        if (request.ElderId.HasValue) medication.ElderId = request.ElderId.Value;
        
        var result  = await _repository.UpdateMedicationAsync(medication);
        
        var medicationWithRelations = await _repository.GetMedicationByIdWithRelationsAsync(result.MedicationId);

        var medicationDto = _mapper.Map<ReadMedicationDto>(medicationWithRelations);

        return Result<ReadMedicationDto>.Success(medicationDto); 
    }
}