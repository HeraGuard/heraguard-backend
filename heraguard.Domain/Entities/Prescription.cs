namespace heraguard.Domain.Entities;

public class Prescription
{
    public Guid Id { get; set; }
    public Guid ElderId { get; set; }
    public Guid? DoctorId { get; set; }
    public DateTime Date { get; set; }
    public List<Medication> Medications { get; set; } = new List<Medication>();
    
    public ElderProfile Elder { get; set; }  
    public DoctorProfile? Doctor { get; set; }
}