using AutoMapper;
using heraguard.Application.Medications.Commands;
using heraguard.Application.Medications.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping;

public class MedicationProfile : Profile
{
    public MedicationProfile()
    {
        CreateMap<Medication, ReadMedicationDto>();
        
        CreateMap<CreateMedicationDto, Medication>()
            .ForMember(dest => dest.MedicationId, opt => opt.Ignore());
        
        CreateMap<CreateMedicationCommand, Medication>()
            .ForMember(dest => dest.MedicationId, opt => opt.Ignore());
        
        CreateMap<UpdateMedicationDto, Medication>()
            .ForMember(dest => dest.MedicationId, opt => opt.Ignore());
        
        CreateMap<Medication, UpdateMedicationDto>();
    }
}