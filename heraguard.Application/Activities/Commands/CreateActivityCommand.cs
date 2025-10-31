using MediatR;
using heraguard.Application.Activities.Dtos;
using heraguard.Domain.Common;

namespace heraguard.Application.Activities.Commands;

public class CreateActivityCommand : IRequest<Result<ReadActivityDto>>
{
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public TimeSpan RecommendedTime { get; set; }
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid? ElderId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? CaregiverId { get; set; }

    public CreateActivityCommand() { }

    public CreateActivityCommand(string name, string frequency, TimeSpan recommendedTime, string duration, string notes, Guid elderId, Guid doctorId, Guid caregiverId)
    {
        Name = name;
        Frequency = frequency;
        RecommendedTime = recommendedTime;
        Duration = duration;
        Notes = notes;
        ElderId = elderId;
        DoctorId = doctorId;
        CaregiverId = caregiverId;
    }
}