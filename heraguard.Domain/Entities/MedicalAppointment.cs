namespace heraguard.Domain.Entities;

public class MedicalAppointment
{
    public Guid MedicalAppointmentId { get; set; }
    public string NameOfPatient { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string? Description { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }
    public Guid ElderId { get; set; }
    public DoctorProfile? DoctorProfile { get; set; }
    public CaregiverProfile? CaregiverProfile { get; set; }
    public ElderProfile ElderProfile { get; set; }
}

