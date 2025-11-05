using AutoMapper;
using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Application.MedicalAppointments.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.MedicalAppointments.Handlers;

public class GetByIdMedicalAppointmentHandler : IRequestHandler<GetMedicalAppointmentByIdQuery, Result<ReadMedicalAppointmentDto>>
{
    private readonly IMedicalAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdMedicalAppointmentHandler(IMedicalAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ReadMedicalAppointmentDto>> Handle(GetMedicalAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var medicalAppointment = await _repository.GetMedicalAppointmentByIdAsync(request.MedicalAppointmentId);

        if (medicalAppointment == null)
            return Result<ReadMedicalAppointmentDto>.Failure(MedicalAppointmentErrors.NotFound);

        var dto = _mapper.Map<ReadMedicalAppointmentDto>(medicalAppointment);
        return Result<ReadMedicalAppointmentDto>.Success(dto);
    }
}

