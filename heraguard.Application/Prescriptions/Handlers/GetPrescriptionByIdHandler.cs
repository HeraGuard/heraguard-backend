using AutoMapper;
using heraguard.Application.Prescriptions.Dtos;
using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Application.Prescriptions.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Prescriptions.Handlers;

public class GetPrescriptionByIdHandler: IRequestHandler<GetPrescriptionByIdQuery, Result<ReadPrescriptionDto>>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;

    public GetPrescriptionByIdHandler(IPrescriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<ReadPrescriptionDto>> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _repository.GetPrescriptionByIdAsync(request.PrescriptionId);
        
        if (prescription == null)
            return Result<ReadPrescriptionDto>.Failure(PrescriptionErrors.NotFound);

        var prescriptionDto = _mapper.Map<ReadPrescriptionDto>(prescription);
        
        return Result<ReadPrescriptionDto>.Success(prescriptionDto);
    }
}