using AutoMapper;
using heraguard.Domain.Entities;
using heraguard.Application.Users.DTOs;

namespace heraguard.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            // Mapeo desde ElderProfile a UserDto usando la navegación a User
            CreateMap<ElderProfile, UserDto>()
                .ConstructUsing(src => src.User != null
                    ? new UserDto
                    {
                        Id = src.User.Id, // <-- Asegura llenar el Id
                        Name = src.User.Name,
                        LastName = src.User.LastName,
                        Email = src.User.Email,
                        Role = src.User.Role != null ? src.User.Role.Name : null
                    }
                    : new UserDto());


            // Mapeos similares para perfiles relacionados
            CreateMap<DoctorProfile, UserDto>()
                .ConstructUsing(src => src.User != null
                    ? new UserDto
                    {
                        Name = src.User.Name,
                        LastName = src.User.LastName,
                        Email = src.User.Email,
                        Role = src.User.Role != null ? src.User.Role.Name : null
                    }
                    : new UserDto());

            CreateMap<CaregiverProfile, UserDto>()
                .ConstructUsing(src => src.User != null
                    ? new UserDto
                    {
                        Name = src.User.Name,
                        LastName = src.User.LastName,
                        Email = src.User.Email,
                        Role = src.User.Role != null ? src.User.Role.Name : null
                    }
                    : new UserDto());
        }
    }
}