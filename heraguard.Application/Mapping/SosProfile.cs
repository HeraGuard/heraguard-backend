using AutoMapper;
using heraguard.Application.Sos.Dtos;
using heraguard.Domain.Entities;

namespace heraguard.Application.Mapping;

public class SosProfile: Profile
{
    public SosProfile()
    {
        CreateMap<SosEvent, SosEventDto>();
    }
}