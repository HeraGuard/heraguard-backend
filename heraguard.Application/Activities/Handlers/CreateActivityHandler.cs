using AutoMapper;
using heraguard.Domain.Entities;
using MediatR;
using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Commands;
using heraguard.Domain.Common;
using heraguard.Application.Activities.Interfaces;

namespace heraguard.Application.Activities.Handlers;

public class CreateActivityHandler : IRequestHandler<CreateActivityCommand, Result<ReadActivityDto>>
{
    private readonly IActivityRepository _repository;
    private readonly IMapper _mapper;

    public CreateActivityHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ReadActivityDto>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = _mapper.Map<Activity>(request);
        activity.ActivityId = Guid.NewGuid();
        var resutl = await _repository.AddActivityAsync(activity);
        var activityDto = _mapper.Map<ReadActivityDto>(resutl);
        return Result<ReadActivityDto>.Success(activityDto);
    }
}