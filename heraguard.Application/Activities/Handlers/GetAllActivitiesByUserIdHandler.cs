using AutoMapper;
using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Interfaces;
using heraguard.Application.Activities.Queries;
using heraguard.Domain.Common;
using MediatR;

namespace heraguard.Application.Activities.Handlers;

public class GetAllActivitiesByUserIdHandler : IRequestHandler<GetAllActivitiesByUserIdQuery, Result<List<ReadActivityDto>>>
{
    public readonly IActivityRepository _repository;
    public readonly IMapper _mapper;

    public GetAllActivitiesByUserIdHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<ReadActivityDto>>> Handle(GetAllActivitiesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var activities = await _repository.GetAllActivitiesByUserIdAsync(request.UserId);
        var activitiesDtos = _mapper.Map<List<ReadActivityDto>>(activities);
        return Result<List<ReadActivityDto>>.Success(activitiesDtos);
    }
}