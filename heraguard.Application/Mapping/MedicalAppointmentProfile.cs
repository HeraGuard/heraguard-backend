using AutoMapper;
using heraguard.Application.MedicalAppointments.Dtos;
using heraguard.Application.MedicalAppointments.Commands;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping;

public class MedicalAppointmentProfile : Profile
{
    public MedicalAppointmentProfile()
    {
        CreateMap<MedicalAppointment, ReadMedicalAppointmentDto>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                src.DoctorProfile != null ? src.DoctorProfile.User.Name + " " + src.DoctorProfile.User.LastName : null))
            .ForMember(dest => dest.CaregiverName, opt => opt.MapFrom(src =>
                src.CaregiverProfile != null ? src.CaregiverProfile.User.Name + " " + src.CaregiverProfile.User.LastName : null))
            .ForMember(dest => dest.ElderName, opt => opt.MapFrom(src =>
                src.ElderProfile.User.Name + " " + src.ElderProfile.User.LastName));
        CreateMap<CreateMedicalAppointmentDto, MedicalAppointment>()
            .ForMember(dest => dest.MedicalAppointmentId, opt => opt.Ignore());
        CreateMap<CreateMedicalAppointmentCommand, MedicalAppointment>()
            .ForMember(dest => dest.MedicalAppointmentId,opt => opt.Ignore());
        CreateMap<UpdateMedicalAppointmentDto, MedicalAppointment>()
            .ForMember(dest => dest.MedicalAppointmentId, opt => opt.Ignore());
        CreateMap<MedicalAppointment, UpdateMedicalAppointmentDto>();
    }
}

