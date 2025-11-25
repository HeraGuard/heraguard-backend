using AutoMapper;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Application.Prescriptions.Commands;
using heraguard.Application.Prescriptions.Dtos;
using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Domain.Common;
using heraguard.Domain.Entities;
using MediatR;

namespace heraguard.Application.Prescriptions.Handlers;

public class CreatePrescriptionHandler: IRequestHandler<CreatePrescriptionCommand, Result<ReadPrescriptionDto>>
{
    private readonly IPrescriptionRepository _repository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public CreatePrescriptionHandler(IPrescriptionRepository repository, IMapper mapper, INotificationService notificationService)
    {
        _repository = repository;
        _mapper = mapper;
        _notificationService = notificationService;
    }
    
    public async Task<Result<ReadPrescriptionDto>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = _mapper.Map<Prescription>(request);
        prescription.Id = Guid.NewGuid();

        var result = await _repository.addPrescriptionAsync(prescription);
        
        if (result.Medications != null && result.Medications.Any())
        {
            foreach (var medication in result.Medications)
            {
                try
                {
                    await _notificationService.ScheduleInitialNotificationsAsync(
                        medication.MedicationId,
                        result.ElderId
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error programando notificaciones: {ex.Message}");
                    // No fallar toda la operación
                }
            }
        }
        
        var prescriptionDto = _mapper.Map<ReadPrescriptionDto>(result);
        return Result<ReadPrescriptionDto>.Success(prescriptionDto); 
        
    }
}