using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.MedicalAppointments.Commands;

public class CreateMedicalAppointmentCommand : IRequest<Result<ReadMedicalAppointmentDto>>
{
    public string NameOfPatient { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string? Description { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid ElderId { get; set; }

    public CreateMedicalAppointmentCommand(string nameOfPatient, DateOnly date, TimeOnly time, string? description, Guid? doctorId, Guid? caregiverId, Guid elderId)
    {
        NameOfPatient = nameOfPatient;
        Date = date;
        Time = time;
        Description = description;
        DoctorId = doctorId;
        CaregiverId = caregiverId;
        ElderId = elderId;
    }
}

