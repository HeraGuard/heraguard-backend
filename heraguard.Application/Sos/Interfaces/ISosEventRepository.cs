using heraguard.Domain.Entities;

namespace heraguard.Application.Sos.Interfaces;

public interface ISosEventRepository
{
    Task<SosEvent> CreateAsync(SosEvent sosEvent);
}