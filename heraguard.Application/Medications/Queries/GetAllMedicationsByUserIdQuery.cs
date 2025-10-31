using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Medications.Queries;

public class GetAllMedicationsByUserIdQuery : IRequest<Result<List<ReadMedicationDto>>>
{
    public Guid UserId { get; set; }
    
    public GetAllMedicationsByUserIdQuery (Guid userId)
    {
        UserId = userId;
    }
}