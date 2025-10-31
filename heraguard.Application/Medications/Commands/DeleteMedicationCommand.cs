using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Medications.Commands;

public class DeleteMedicationCommand : IRequest<Result<Unit>>
{
    public Guid MedicationId { get; set; }
    
    public DeleteMedicationCommand(Guid medicationId)
    {
        MedicationId = medicationId;
    }
}