using MediatR;

public class UpdateActivityCommand : IRequest<ActivityCreateDto>
{
    public ActivityUpdateDto ActivityUpdateDto { get; }

    public UpdateActivityCommand(ActivityUpdateDto activityUpdateDto)
    {
        ActivityUpdateDto = activityUpdateDto;
    }
}