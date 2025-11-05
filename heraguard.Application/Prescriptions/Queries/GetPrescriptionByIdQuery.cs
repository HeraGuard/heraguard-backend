using heraguard.Application.Prescriptions.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Prescriptions.Queries;

public class GetPrescriptionByIdQuery : IRequest<Result<ReadPrescriptionDto>>
{
    public Guid PrescriptionId { get; set; }

    public GetPrescriptionByIdQuery(Guid prescriptionId)
    {
        PrescriptionId = prescriptionId;
    }
    
}