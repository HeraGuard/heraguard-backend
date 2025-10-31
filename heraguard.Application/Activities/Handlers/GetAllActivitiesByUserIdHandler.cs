using AutoMapper;
using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Interfaces;
using MediatR;

namespace heraguard.Application.Activities.Handlers;

public class GetAllActivitiesByUserIdHandler : IRequestHandler<GetAllActivitiesByUserIdQuery, IEnumerable<CreateActivityDto>>
{
    public readonly IActivityRepository _repository;
    public readonly IMapper _mapper;
    public GetAllActivitiesByUserIdHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CreateActivityDto>> Handle(GetAllActivitiesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var activities = await _repository.GetAllActivitiesByUserIdAsync(request.Id);
        var activityCreateDto = _mapper.Map<IEnumerable<CreateActivityDto>>(activities);
        return activityCreateDto;
    }
}