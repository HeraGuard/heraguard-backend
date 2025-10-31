using AutoMapper;
using heraguard.Application.Medications.Dtos;
using heraguard.Application.Medications.Interfaces;
using heraguard.Application.Medications.Queries;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Medications.Handlers;

public class GetAllMedicationsByUserIdHandler: IRequestHandler<GetAllMedicationsByUserIdQuery, Result<List<ReadMedicationDto>>>
{
    private readonly IMedicationRepository _repository;
    private readonly IMapper _mapper;

    public GetAllMedicationsByUserIdHandler(IMedicationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<ReadMedicationDto>>> Handle(GetAllMedicationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var medications = await _repository.GetAllMedicationsByUserIdAsync(request.UserId);
        
        var medicationDtos = _mapper.Map<List<ReadMedicationDto>>(medications);

        return Result<List<ReadMedicationDto>>.Success(medicationDtos);
    }
}