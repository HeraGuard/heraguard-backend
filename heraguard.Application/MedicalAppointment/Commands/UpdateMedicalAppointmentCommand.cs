using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.MedicalAppointments.Commands;

public class UpdateMedicalAppointmentCommand : IRequest<Result<ReadMedicalAppointmentDto>>
{
    public Guid MedicalAppointmentId { get; set; }
    public string? NameOfPatient { get; set; }
    public DateOnly? Date { get; set; }
    public TimeOnly? Time { get; set; }
    public string? Description { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid? ElderId { get; set; }

    public UpdateMedicalAppointmentCommand(Guid medicalAppointmentId, string? nameOfPatient, DateOnly? date, TimeOnly? time, string? description, Guid? doctorId, Guid? caregiverId, Guid? elderId)
    {
        MedicalAppointmentId = medicalAppointmentId;
        NameOfPatient = nameOfPatient;
        Date = date;
        Time = time;
        Description = description;
        DoctorId = doctorId;
        CaregiverId = caregiverId;
        ElderId = elderId;
    }
}

