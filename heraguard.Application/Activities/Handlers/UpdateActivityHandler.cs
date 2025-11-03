using AutoMapper;
using heraguard.Application.Activities.Commands;
using heraguard.Application.Activities.Dtos;
using heraguard.Application.Activities.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Common.Errors;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Activities.Handlers;

public class UpdateActivityHandler : IRequestHandler<UpdateActivityCommand, Result<ReadActivityDto>>
{
    private readonly IActivityRepository _repository;
    public readonly IMapper _mapper;

    public UpdateActivityHandler(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ReadActivityDto>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _repository.GetActivityByIdAsync(request.ActivityId);
        if (activity == null) return Result<ReadActivityDto>.Failure(ActivityErrors.NotFound);

        if (request.Name != null) activity.Name = request.Name;
        if (request.Frequency != null) activity.Frequency = request.Frequency;
        if (request.RecommendedTime.HasValue) activity.RecommendedTime = request.RecommendedTime.Value;
        if (request.Duration != null) activity.Duration = request.Duration;
        if (request.Notes != null) activity.Notes = request.Notes;
        if (request.ElderId.HasValue) activity.ElderId = request.ElderId.Value;
        if (request.DoctorId.HasValue) activity.DoctorId = request.DoctorId;
        if (request.CaregiverId.HasValue) activity.CaregiverId = request.CaregiverId;

        var result = await _repository.UpdateActivityAsync(activity);
        var activityWithRealtions = await _repository.GetActivityByIdWithRelationsAsync(result.ActivityId);
        var activityDto = _mapper.Map<ReadActivityDto>(activityWithRealtions);
        return Result<ReadActivityDto>.Success(activityDto);
    }
}