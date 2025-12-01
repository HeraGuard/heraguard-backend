using AutoMapper;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Application.Sos.Commands;
using heraguard.Application.Sos.Dtos;
using heraguard.Application.Sos.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Sos.Handlers;

public class CreateSosEventHandler: IRequestHandler<CreateSosEventCommand, Result<SosEventDto>>
{
    private readonly ISosEventRepository _sosRepository;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public CreateSosEventHandler(
        ISosEventRepository sosRepository,
        INotificationService notificationService,
        IMapper mapper)
    {
        _sosRepository = sosRepository;
        _notificationService = notificationService;
        _mapper = mapper;
    }

    public async Task<Result<SosEventDto>> Handle(
        CreateSosEventCommand request, 
        CancellationToken cancellationToken)
    {
        var sos = new SosEvent
        {
            Id = Guid.NewGuid(),
            ElderId = request.ElderId,
            CreatedAt = DateTime.UtcNow,
            Status = "pending",
            Notes = request.Notes
        };

        sos = await _sosRepository.CreateAsync(sos);
        
        await _notificationService.SendSosAlertAsync(request.ElderId, sos.Id);


        var dto = _mapper.Map<SosEventDto>(sos);
        return Result<SosEventDto>.Success(dto);
    }
}