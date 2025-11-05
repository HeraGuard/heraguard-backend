namespace heraguard.Application.MedicalAppointments.Dtos;

public class UpdateMedicalAppointmentDto
{
    public Guid MedicalAppointmentId { get; set; }
    public string? NameOfPatient { get; set; }
    public DateOnly? Date { get; set; }
    public TimeOnly? Time { get; set; }
    public string? Description { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid? ElderId { get; set; }
}

