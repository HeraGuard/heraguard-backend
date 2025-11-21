using AutoMapper;
using heraguard.Domain.Entities;
using heraguard.Application.Users.DTOs;

namespace heraguard.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateMap<User, UserDto>()
            //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //     .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //     .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            // // Mapeo desde ElderProfile a UserDto usando la navegación a User
            // CreateMap<ElderProfile, UserDto>()
            //     .ConstructUsing(src => src.User != null
            //         ? new UserDto
            //         {
            //             Id = src.User.Id, // <-- Asegura llenar el Id
            //             Name = src.User.Name,
            //             LastName = src.User.LastName,
            //             Email = src.User.Email,
            //             Role = src.User.Role != null ? src.User.Role.Name : null
            //         }
            //         : new UserDto());


            // // Mapeos similares para perfiles relacionados
            // CreateMap<DoctorProfile, UserDto>()
            //     .ConstructUsing(src => src.User != null
            //         ? new UserDto
            //         {
            //             Name = src.User.Name,
            //             LastName = src.User.LastName,
            //             Email = src.User.Email,
            //             Role = src.User.Role != null ? src.User.Role.Name : null
            //         }
            //         : new UserDto());

            // CreateMap<CaregiverProfile, UserDto>()
            //     .ConstructUsing(src => src.User != null
            //         ? new UserDto
            //         {
            //             Name = src.User.Name,
            //             LastName = src.User.LastName,
            //             Email = src.User.Email,
            //             Role = src.User.Role != null ? src.User.Role.Name : null
            //         }
            //         : new UserDto());

                    CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
            // Elder
            .ForMember(dest => dest.LinkingCode, opt => opt.MapFrom(src =>
                src.AdultoMayorProfile != null ? src.AdultoMayorProfile.LinkingCode : null))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src =>
            src.AdultoMayorProfile != null ? src.AdultoMayorProfile.DateOfBirth : default(DateTime)))
            .ForMember(dest => dest.EmergencyContact, opt => opt.MapFrom(src =>
                src.AdultoMayorProfile != null ? src.AdultoMayorProfile.EmergencyContact : null))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                src.AdultoMayorProfile != null ? src.AdultoMayorProfile.Address : null))
            // Doctor
            .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src =>
                src.DoctorProfile != null ? src.DoctorProfile.Specialty : null))
            .ForMember(dest => dest.MedicalLicense, opt => opt.MapFrom(src =>
                src.DoctorProfile != null ? src.DoctorProfile.MedicalLicense : null))
            .ForMember(dest => dest.MedicalCenter, opt => opt.MapFrom(src =>
                src.DoctorProfile != null ? src.DoctorProfile.MedicalCenter : null))
            // Caregiver
            .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src =>
                src.FamiliarProfile != null ? src.FamiliarProfile.Relationship : null))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src =>
                src.FamiliarProfile != null ? src.FamiliarProfile.PhoneNumber : null));
                }
        }
    }
