using AutoMapper;
using heraguard.Application.Medications.Dtos;
using heraguard.Application.Medications.Interfaces;
using heraguard.Application.Medications.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Medications.Handlers;

public class GetMedicationByIdHandler: IRequestHandler<GetMedicationByIdQuery, Result<ReadMedicationDto>>
{
    private readonly IMedicationRepository _repository;
    private readonly IMapper _mapper;

    public GetMedicationByIdHandler(IMedicationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<ReadMedicationDto>> Handle(GetMedicationByIdQuery request, CancellationToken cancellationToken)
    {
        
        var medication = await _repository.GetMedicationByIdWithRelationsAsync(request.MedicationId);
        
        if (medication == null)
            return Result<ReadMedicationDto>.Failure(MedicationErrors.NotFound);
        
        var medicationDto =  _mapper.Map<ReadMedicationDto>(medication);
        return Result<ReadMedicationDto>.Success(medicationDto);
    }
}