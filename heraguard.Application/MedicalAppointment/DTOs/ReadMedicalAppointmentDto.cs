namespace heraguard.Application.MedicalAppointments.Dtos;

public class ReadMedicalAppointmentDto
{
    public Guid MedicalAppointmentId { get; set; }
    public string NameOfPatient { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string? Description { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid ElderId { get; set; }
    public string? DoctorName { get; set; }
    public string? CaregiverName { get; set; }
    public string ElderName { get; set; } = string.Empty;
}

