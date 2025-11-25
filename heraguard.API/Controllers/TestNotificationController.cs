using heraguard.Application.Notifications.Interfaces;
using heraguard.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace heraguard.API.Controllers;

// TestNotificationController.cs
[ApiController]
[Route("api/[controller]")]
public class TestNotificationController : ControllerBase
{
    private readonly INotificationService _firebaseService;

    public TestNotificationController(INotificationService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    [HttpPost]
    public async Task<IActionResult> SendTest([FromBody] string token)
    {
        await _firebaseService.SendTestNotificationAsync(token);
        return Ok("Notificación enviada (si el token es válido)");
    }
    
    [HttpPost("{scheduleId}")]
    public async Task<IActionResult> SendNotification(Guid scheduleId)
    {
        await _firebaseService.SendNotificationAsync(scheduleId);
        return Ok("Notificación enviada desde el flujo Hangfire (si el token es válido y todo está bien inicializado)");
    }
}
