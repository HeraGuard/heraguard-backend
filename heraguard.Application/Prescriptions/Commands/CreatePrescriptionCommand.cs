using heraguard.Application.Medications.Dtos;
using heraguard.Application.Prescriptions.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Prescriptions.Commands;

public class CreatePrescriptionCommand : IRequest<Result<ReadPrescriptionDto>>
{
    public Guid ElderId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public List<CreateMedicationDto> Medications { get; set; }


    public CreatePrescriptionCommand(Guid elderId, Guid doctorId, DateTime date,  List<CreateMedicationDto> medications)
    {
        ElderId = elderId;
        DoctorId = doctorId;
        Date = date;
        Medications = medications;
    }
}