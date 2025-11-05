using AutoMapper;
using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Application.MedicalAppointments.Queries;
using heraguard.Domain.Common;
using MediatR;


namespace heraguard.Application.MedicalAppointments.Handlers;

public class GetAllMedicalAppointmentsByUserIdHandler : IRequestHandler<GetAllMedicalAppointmentsByUserIdQuery, Result<List<ReadMedicalAppointmentDto>>>
{
    private readonly IMedicalAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public GetAllMedicalAppointmentsByUserIdHandler(IMedicalAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<ReadMedicalAppointmentDto>>> Handle(GetAllMedicalAppointmentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var MedicalAppointments = await _repository.GetAllMedicalAppointmentByUserIdAsync(request.UserId);
        var medicalAppointmentDtos = _mapper.Map<List<ReadMedicalAppointmentDto>>(MedicalAppointments);
        return Result<List<ReadMedicalAppointmentDto>>.Success(medicalAppointmentDtos);
    }
}

