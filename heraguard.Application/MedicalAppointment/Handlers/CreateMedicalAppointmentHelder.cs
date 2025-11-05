using AutoMapper;
using heraguard.Application.MedicalAppointments.Commands;
using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.MedicalAppointments.Handlers;

public class CreateMedicalAppointmentHandler : IRequestHandler<CreateMedicalAppointmentCommand, Result<ReadMedicalAppointmentDto>>
{
    private readonly IMedicalAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public CreateMedicalAppointmentHandler(IMedicalAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ReadMedicalAppointmentDto>> Handle(CreateMedicalAppointmentCommand request, CancellationToken cancellationToken)
    {
        var medicalAppointment = _mapper.Map<MedicalAppointment>(request);
        medicalAppointment.MedicalAppointmentId = Guid.NewGuid();

        var result = await _repository.AddMedicalAppointmentAsync(medicalAppointment);

        var medicalAppointmentWithRelations = await _repository.GetMedicalAppointmentByIdWithRelationAsync(result.MedicalAppointmentId);

        var medicalAppointmentDto = _mapper.Map<ReadMedicalAppointmentDto>(medicalAppointmentWithRelations);

        return Result<ReadMedicalAppointmentDto>.Success(medicalAppointmentDto);
    }
}

