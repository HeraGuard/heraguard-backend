using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Medications.Queries;

public class GetMedicationByIdQuery : IRequest<Result<ReadMedicationDto>>
{
    public Guid MedicationId { get; set; }
    
    public GetMedicationByIdQuery(Guid medicationId)
    {
        MedicationId = medicationId;
    }
}