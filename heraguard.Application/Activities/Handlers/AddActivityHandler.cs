using AutoMapper;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Activities.Handlers;

public class AddActivityHandler : IRequestHandler<AddActivityCommand, ActivityCreateDto>
{
    private readonly IActivityRepository _repository;
    private readonly IMapper _mapper;

    public AddActivityHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ActivityCreateDto> Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
        var activityEntity = _mapper.Map<Activity>(request.ActivityCreateDto);
        activityEntity.DoctorID = request.DoctorId;
        var addedActivity = await _repository.CreateActivityAsync(activityEntity);
        var activityDto = _mapper.Map<ActivityCreateDto>(addedActivity);
        return activityDto;
    }
}