using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Interfaces;
using heraguard.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace heraguard.Application.Auth.Handlers;

public class LogoutHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IAuthService _authService;
    private readonly ILogger<LogoutHandler> _logger;

    public LogoutHandler(IAuthService authService, ILogger<LogoutHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing logout for user: {UserId}", request.UserId);
        
        return await _authService.LogoutAsync();
    }
}

