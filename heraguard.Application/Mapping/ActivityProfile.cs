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
            CreateMap<Activity, ReadActivityDto>()
                .ForMember(dest => dest.DoctorName,
                    opt => opt.MapFrom(src => src.DoctorProfile != null
                        ? src.DoctorProfile.User.Name + " " + src.DoctorProfile.User.LastName
                        : null))
                .ForMember(dest => dest.ElderName,
                    opt => opt.MapFrom(src => src.ElderProfile.User.Name + " " + src.ElderProfile.User.LastName))
                .ForMember(dest => dest.CaregiverName,
                    opt => opt.MapFrom(src => src.CaregiverProfile != null
                        ? src.CaregiverProfile.User.Name + " " + src.CaregiverProfile.User.LastName
                        : null));

            CreateMap<CreateActivityDto, Activity>()
                .ForMember(dest => dest.ActivityId, opt => opt.Ignore());

            CreateMap<UpdateActivityDto, Activity>()
                .ForMember(dest => dest.ActivityId, opt => opt.Ignore());

            CreateMap<CreateActivityCommand, Activity>()
               .ForMember(dest => dest.ActivityId, opt => opt.Ignore());
        }
    }
}