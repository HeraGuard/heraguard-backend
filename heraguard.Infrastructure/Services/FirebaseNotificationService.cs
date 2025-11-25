using FirebaseAdmin.Messaging;
using heraguard.Application.Medications.Interfaces;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Domain.Entities;
using heraguard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace heraguard.Infrastructure.Services;


public class FirebaseNotificationService : INotificationService
{
    private readonly IUserDeviceTokenRepository _tokenRepository;
    private readonly IMedicationScheduleRepository _scheduleRepository;
    private readonly IMedicationRepository _medicationRepository;
    private readonly ICaregiverAlertRepository _caregiverAlertRepository;
    private readonly IBackgroundJobScheduler _jobScheduler;
    private readonly HeraGuardDbContext _context;

    public FirebaseNotificationService(
        IUserDeviceTokenRepository tokenRepository,
        IMedicationScheduleRepository scheduleRepository,
        IMedicationRepository medicationRepository,
        ICaregiverAlertRepository caregiverAlertRepository,
        IBackgroundJobScheduler jobScheduler,
        HeraGuardDbContext context)
    {
        _tokenRepository = tokenRepository;
        _scheduleRepository = scheduleRepository;
        _medicationRepository = medicationRepository;
        _caregiverAlertRepository = caregiverAlertRepository;
        _jobScheduler = jobScheduler;
        _context = context;
    }
    
    // TODO 

    public async Task SendTestNotificationAsync(string fcmToken)
    {
        var message = new Message
        {
            Token = fcmToken,
            Notification = new Notification
            {
                Title = "Notificación de prueba",
                Body = "Test de notificacion"
            }
        };

    }


    public async Task ScheduleInitialNotificationsAsync(Guid medicationId, Guid elderId)
    {
        var medication = await _medicationRepository.GetMedicationByIdAsync(medicationId);
        if (medication == null) return;

        // Verificar cuántas notificaciones pendientes ya hay
        var pendingCount = await _scheduleRepository.CountPendingByMedicationAsync(medicationId);
        var toCreate = Math.Max(0, 3 - pendingCount); // Mantener siempre 3 pendientes

        if (toCreate == 0) return;

        // Obtener la última notificación programada
        var existingSchedules = await _scheduleRepository.GetPendingByMedicationAsync(medicationId);
        var lastScheduledTime = existingSchedules.Any() 
            ? existingSchedules.Max(s => s.ScheduledTime) 
            : medication.StartDate;

        // Calcular fecha de fin
        var endDate = medication.StartDate.AddDays(medication.Duration);

        // Generar las próximas notificaciones
        var currentTime = lastScheduledTime;
        for (int i = 0; i < toCreate; i++)
        {
            // Si es la primera, empezar desde StartDate, si no, sumar frecuencia
            if (i > 0 || existingSchedules.Any())
            {
                currentTime = currentTime.AddHours(medication.Frequency);
            }

            if (currentTime > endDate) break;

            var schedule = new MedicationSchedule
            {
                Id = Guid.NewGuid(),
                MedicationId = medicationId,
                ElderId = elderId,
                ScheduledTime = currentTime,
                NotificationSent = false,
                CriticalAlertSent = false,
                Status = "pending",
                CreatedAt = DateTime.UtcNow
            };

            await _scheduleRepository.AddAsync(schedule);

            // Programar notificación en Hangfire
            if (currentTime > DateTime.UtcNow)
            {
                _jobScheduler.ScheduleJob<INotificationService>(
                    (INotificationService service) => service.SendNotificationAsync(schedule.Id),
                    currentTime
                );

                // Programar alarma crítica 15 min después
                var criticalAlertTime = currentTime.AddMinutes(15);
                _jobScheduler.ScheduleJob<INotificationService>(
                    (INotificationService service) => service.SendCriticalAlertAsync(schedule.Id),
                    criticalAlertTime
                );

            }
        }
    }

    public async Task SendNotificationAsync(Guid scheduleId)
    //Todo debug
    {
        var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null || schedule.Status != "pending") return;
        var medication = await _medicationRepository.GetMedicationByIdAsync(schedule.MedicationId);
        if (medication == null) return;
        
        var deviceTokens = await _tokenRepository.GetDeviceTokensByUserIdAsync(schedule.ElderId);
        
        if (!deviceTokens.Any())
        {
            return;
        }
        

        try
        {
            /*
            var message = new MulticastMessage
            {
                Tokens = deviceTokens,
                Notification = new Notification
                {
                    Title = $"💊 {medication.Name}",
                    Body = $"Es hora de tomar tu medicamento - {medication.Dosage}"
                },
                Data = new Dictionary<string, string>
                {
                    { "type", "medication_reminder" },
                    { "scheduleId", scheduleId.ToString() },
                    { "medicationId", medication.MedicationId.ToString() },
                    { "priority", "normal" }
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        Sound = "default",
                        ChannelId = "medication_channel"
                    }
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Sound = "default",
                        Badge = 1
                    }
                }
            };*/
            
            var message = new Message
            {
                Token = deviceTokens.First(),
                Notification = new Notification
                {
                    Title = $"💊 {medication.Name}",
                    Body = $"Es hora de tomar tu medicamento - {medication.Dosage}"
                },
                Data = new Dictionary<string, string>
                {
                    { "type", "medication_reminder" },
                    { "scheduleId", scheduleId.ToString() },
                    { "medicationId", medication.MedicationId.ToString() },
                    { "priority", "normal" }
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        Sound = "default",
                        ChannelId = "medication_channel"
                    }
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Sound = "default",
                        Badge = 1
                    }
                }
            };

            //var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine($"FCM Response: {response}");

            
            schedule.NotificationSent = true;
            await _scheduleRepository.UpdateAsync(schedule);

           // Console.WriteLine($"✅ Notificación enviada: {response.SuccessCount} exitosas");

            // Limpiar tokens inválidos
            /*
            if (response.FailureCount > 0)
            {
                for (int i = 0; i < response.Responses.Count; i++)
                {
                    if (!response.Responses[i].IsSuccess)
                    {
                        await _tokenRepository.RemoveInvalidTokenAsync(deviceTokens[i]);
                    }
                }
            }*/
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error enviando notificación: {ex.Message}");
        }
    }

    public async Task SendCriticalAlertAsync(Guid scheduleId)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null || schedule.Status != "pending") return;

        // Si ya fue confirmada, no enviar alarma
        if (schedule.Status != "pending") return;

        var medication = await _medicationRepository.GetMedicationByIdAsync(schedule.MedicationId);
        if (medication == null) return;

        // Verificar si el elder tiene las alarmas críticas activadas
        var elder = await _context.Elders.FindAsync(schedule.ElderId);
        if (elder == null || !elder.EnableCriticalAlerts)
        {
            Console.WriteLine($"Alarmas críticas desactivadas para elder {schedule.ElderId}");
            
            // Aunque no envíe alarma, notificar al cuidador
            await NotifyCaregiversAsync(
                schedule.ElderId, 
                schedule.MedicationId, 
                $"El adulto mayor no ha confirmado su medicamento: {medication.Name}"
            );
            return;
        }

        var deviceTokens = await _tokenRepository.GetDeviceTokensByUserIdAsync(schedule.ElderId);
        if (!deviceTokens.Any()) return;

        try
        {
            var message = new MulticastMessage
            {
                Tokens = deviceTokens,
                Notification = new Notification
                {
                    Title = $"⚠️ RECORDATORIO IMPORTANTE",
                    Body = $"No olvidar: {medication.Name} - {medication.Dosage}"
                },
                Data = new Dictionary<string, string>
                {
                    { "type", "medication_critical_alert" },
                    { "scheduleId", scheduleId.ToString() },
                    { "medicationId", medication.MedicationId.ToString() },
                    { "priority", "critical" },
                    { "fullScreenIntent", "true" } // Para Android Full-Screen
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        Sound = "default",
                        ChannelId = "medication_critical_channel",
                        // Estos datos permiten full-screen intent en Android
                        ClickAction = "FLUTTER_NOTIFICATION_CLICK"
                    }
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        Sound = "default",
                        Badge = 1,
                        ContentAvailable = true
                    }
                }
            };

            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            
            schedule.CriticalAlertSent = true;
            await _scheduleRepository.UpdateAsync(schedule);

            Console.WriteLine($"🚨 Alarma crítica enviada: {response.SuccessCount} exitosas");

            // Después de 30 minutos más (45 min total), notificar al cuidador
            var caregiverAlertTime = DateTime.UtcNow.AddMinutes(30);
            _jobScheduler.ScheduleJob<INotificationService>(
                (INotificationService service) => service.NotifyCaregiversAsync(
                    schedule.ElderId, 
                    schedule.MedicationId,
                    $"El adulto mayor no ha confirmado su medicamento después de 45 minutos: {medication.Name}"
                ),
                caregiverAlertTime
            );

        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error enviando alarma crítica: {ex.Message}");
        }
    }

    public async Task NotifyCaregiversAsync(Guid elderId, Guid medicationId, string message)
    {
        // Buscar cuidadores relacionados con este elder
        var caregiverRelationships = await _context.Relationships
            .Where(r => r.ElderId == elderId && r.RelationshipTypeId == 2) // 2 = Cuidador
            .ToListAsync();

        var medication = await _medicationRepository.GetMedicationByIdAsync(medicationId);

        foreach (var relationship in caregiverRelationships)
        {
            // Crear alerta en la BD
            var alert = new CaregiverAlert
            {
                Id = Guid.NewGuid(),
                CaregiverId = relationship.RelatedUserId,
                ElderId = elderId,
                MedicationId = medicationId,
                AlertType = "missed_medication",
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _caregiverAlertRepository.AddAsync(alert);

            // Enviar notificación push al cuidador
            var caregiverTokens = await _tokenRepository.GetDeviceTokensByUserIdAsync(relationship.RelatedUserId);
            
            if (caregiverTokens.Any())
            {
                try
                {
                    var notificationMessage = new MulticastMessage
                    {
                        Tokens = caregiverTokens,
                        Notification = new Notification
                        {
                            Title = "⚠️ Alerta de Medicamento",
                            Body = message
                        },
                        Data = new Dictionary<string, string>
                        {
                            { "type", "caregiver_alert" },
                            { "elderId", elderId.ToString() },
                            { "medicationId", medicationId.ToString() },
                            { "alertId", alert.Id.ToString() }
                        },
                        Android = new AndroidConfig
                        {
                            Priority = Priority.High,
                            Notification = new AndroidNotification
                            {
                                Sound = "default",
                                ChannelId = "caregiver_alerts"
                            }
                        }
                    };

                    await FirebaseMessaging.DefaultInstance.SendMulticastAsync(notificationMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error notificando a cuidador: {ex.Message}");
                }
            }
        }
    }
}