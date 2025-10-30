using AutoMapper;
using MediatR;

namespace heraguard.Application.Activities.Handlers;

public class GetAllActivitiesByUserIdHandler : IRequestHandler<GetAllActivitiesByUserIdQuery, IEnumerable<ActivityCreateDto>>
{
    public readonly IActivityRepository _repository;
    public readonly IMapper _mapper;
    public GetAllActivitiesByUserIdHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ActivityCreateDto>> Handle(GetAllActivitiesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var activities = await _repository.GetAllActivitiesByUserIdAsync(request.Id);
        var activityCreateDto = _mapper.Map<IEnumerable<ActivityCreateDto>>(activities);
        return activityCreateDto;
    }
}