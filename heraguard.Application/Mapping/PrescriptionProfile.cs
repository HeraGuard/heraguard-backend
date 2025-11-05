using heraguard.Application.Prescriptions.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping;

using AutoMapper;

public class PrescriptionProfile : Profile
{
    public PrescriptionProfile()
    {
        CreateMap<CreatePrescriptionDto, Prescription>()
            .ForMember(dest => dest.Medications,
                opt => opt.MapFrom(src => src.Medications)); 
        
        CreateMap<Prescription, ReadPrescriptionDto>()
            .ForMember(dest => dest.Medications,
                opt => opt.MapFrom(src => src.Medications)); 
    }
}
