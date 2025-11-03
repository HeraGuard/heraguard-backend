using AutoMapper;
using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Interfaces;
using heraguard.Application.Activities.Queries;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using MediatR;

namespace heraguard.Application.Activities.Handlers;

public class GetActivityByIdHandler : IRequestHandler<GetActivitByIdyQuery, Result<ReadActivityDto>>
{
    public readonly IActivityRepository _repository;
    public readonly IMapper _mapper;

    public GetActivityByIdHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ReadActivityDto>> Handle(GetActivitByIdyQuery request, CancellationToken cancellationToken)
    {
        var activity = await _repository.GetActivityByIdWithRelationsAsync(request.ActivityId);
        if (activity == null) return Result<ReadActivityDto>.Failure(ActivityErrors.NotFound);
        var activityDto = _mapper.Map<ReadActivityDto>(activity);
        return Result<ReadActivityDto>.Success(activityDto);
    }
}