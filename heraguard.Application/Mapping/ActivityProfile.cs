using AutoMapper;

namespace heraguard.Application.Mapping
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<ActivityCreateDto, Activity>();
            CreateMap<ActivityUpdateDto, Activity>();
        }
    }
}