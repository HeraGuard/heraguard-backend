using MediatR;

public class AddActivityCommand : IRequest<ActivityCreateDto>
{
    public ActivityCreateDto ActivityCreateDto { get; }
    public Guid DoctorId { get; }

    public AddActivityCommand(ActivityCreateDto activityCreateDto, Guid doctorId)
    {
        ActivityCreateDto = activityCreateDto;
        DoctorId = doctorId;
    }
}