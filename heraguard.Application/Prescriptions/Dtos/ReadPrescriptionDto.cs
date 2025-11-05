using heraguard.Application.Medications.Dtos;
using heraguard.Application.Users.DTOs;

namespace heraguard.Application.Prescriptions.Dtos;

public class ReadPrescriptionDto
{
    public Guid Id { get; set; }
    public UserDto Elder { get; set; } = new UserDto();
    public UserDto Doctor { get; set; } = new UserDto();
    public DateTime Date { get; set; }
    public List<ReadMedicationDto> Medications { get; set; } = new List<ReadMedicationDto>();
}