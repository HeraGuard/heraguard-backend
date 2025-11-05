using AutoMapper;
using heraguard.Application.Prescriptions.Commands;
using heraguard.Application.Prescriptions.Dtos;
using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Prescriptions.Handlers;

public class CreatePrescriptionHandler: IRequestHandler<CreatePrescriptionCommand, Result<ReadPrescriptionDto>>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;

    public CreatePrescriptionHandler(IPrescriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<ReadPrescriptionDto>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = _mapper.Map<Prescription>(request);
        prescription.Id = Guid.NewGuid();

        var result = await _repository.addPrescriptionAsync(prescription);
        var prescriptionDto = _mapper.Map<ReadPrescriptionDto>(result);
        return Result<ReadPrescriptionDto>.Success(prescriptionDto); 
        
    }
}