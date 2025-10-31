using AutoMapper;
using heraguard.Application.Activities.Commands;
using heraguard.Application.Activities.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ReadActivityDto>();

            CreateMap<CreateActivityDto, Activity>()
                .ForMember(dest => dest.ActivityId, opt => opt.Ignore());

            CreateMap<UpdateActivityDto, Activity>()
                .ForMember(dest => dest.ActivityId, opt => opt.Ignore());

            CreateMap<CreateActivityCommand, Activity>()
               .ForMember(dest => dest.ActivityId, opt => opt.Ignore());
        }
    }
}