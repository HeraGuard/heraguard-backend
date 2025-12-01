using heraguard.Application.Sos.Dtos;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Sos.Commands;

public class CreateSosEventCommand: IRequest<Result<SosEventDto>>
{
    public Guid ElderId { get; set; }
    public string? Notes { get; set; }

    public CreateSosEventCommand(Guid elderId, string? notes = null)
    {
        ElderId = elderId;
        Notes = notes;
    }
}