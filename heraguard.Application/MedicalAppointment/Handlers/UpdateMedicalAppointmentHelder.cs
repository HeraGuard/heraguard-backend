using AutoMapper;
using heraguard.Application.MedicalAppointments.Commands;
using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;


namespace heraguard.Application.MedicalAppointments.Handlers;

public class UpdateMedicalAppointmentHandler : IRequestHandler<UpdateMedicalAppointmentCommand, Result<ReadMedicalAppointmentDto>>
{
    private readonly IMedicalAppointmentRepository _repository;
    private readonly IMapper _mapper;

    public UpdateMedicalAppointmentHandler(IMedicalAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ReadMedicalAppointmentDto>> Handle(UpdateMedicalAppointmentCommand request, CancellationToken cancellationToken)
    {
        var medicalAppointment = await _repository.GetMedicalAppointmentByIdAsync(request.MedicalAppointmentId);
        if (medicalAppointment == null)
            return Result<ReadMedicalAppointmentDto>.Failure(MedicalAppointmentErrors.NotFound);

        if (request.NameOfPatient != null) medicalAppointment.NameOfPatient = request.NameOfPatient;
        if (request.Date.HasValue) medicalAppointment.Date = request.Date.Value;
        if (request.Time.HasValue) medicalAppointment.Time = request.Time.Value;
        if (request.Description != null) medicalAppointment.Description = request.Description;
        if (request.DoctorId.HasValue) medicalAppointment.DoctorId = request.DoctorId;
        if (request.CaregiverId.HasValue) medicalAppointment.CaregiverId = request.CaregiverId;
        if (request.ElderId.HasValue) medicalAppointment.ElderId = request.ElderId.Value;


        var updated = await _repository.UpdateMedicalAppointmentAsync(medicalAppointment);

        var updatedWithRelations = await _repository.GetMedicalAppointmentByIdAsync(updated.MedicalAppointmentId);

        var dto = _mapper.Map<ReadMedicalAppointmentDto>(updatedWithRelations);

        return Result<ReadMedicalAppointmentDto>.Success(dto);
    }
}

