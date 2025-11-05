using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Prescriptions.Commands;

public class DeletePrescriptionCommand : IRequest<Result<Unit>>
{
    public Guid PrescriptionId { get; set; }

    public DeletePrescriptionCommand(Guid prescriptionId)
    {
        PrescriptionId = prescriptionId;
    }
    
}