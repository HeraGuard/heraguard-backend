using AutoMapper;
using heraguard.Application.Medications.Commands;
using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping;

public class MedicationProfile : Profile
{
    public MedicationProfile()
    {
        CreateMap<Medication, ReadMedicationDto>()
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
        
        CreateMap<CreateMedicationDto, Medication>()
            .ForMember(dest => dest.MedicationId, opt => opt.Ignore());
        
        CreateMap<CreateMedicationCommand, Medication>()
            .ForMember(dest => dest.MedicationId, opt => opt.Ignore());
        
        CreateMap<UpdateMedicationDto, Medication>()
            .ForMember(dest => dest.MedicationId, opt => opt.Ignore());
        
        CreateMap<Medication, UpdateMedicationDto>();
    }
}