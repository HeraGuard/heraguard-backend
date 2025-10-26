namespace heraguard.Domain.Common.Errors;

public static class AuthErrors
{
    // Errores de credenciales
    public static readonly Error InvalidCredentials = new(
        "Auth.InvalidCredentials",
        "Las credenciales son inválidas",
        ErrorType.Unauthorized);
    
    // Errores de email
    public static readonly Error EmailExists = new(
        "Auth.EmailExists",
        "El email ya está registrado",
        ErrorType.Conflict);
    
    // Errores de sesión
    public static readonly Error SessionExpired = new(
        "Auth.SessionExpired",
        "La sesión ha expirado. Por favor inicia sesión nuevamente",
        ErrorType.Unauthorized);
    
    public static readonly Error InvalidToken = new(
        "Auth.InvalidToken",
        "El token proporcionado es inválido",
        ErrorType.Unauthorized);
    
    // Errores de rate limit
    public static readonly Error RateLimitExceeded = new(
        "Auth.RateLimitExceeded",
        "Demasiadas solicitudes. Intenta nuevamente en unos minutos",
        ErrorType.Validation);
    
    // Error genérico
    public static readonly Error AuthenticationFailed = new(
        "Auth.Failed",
        "Error de autenticación. Intenta nuevamente",
        ErrorType.Unauthorized);
    
    // Errores de validación
    public static readonly Error WeakPassword = new(
        "Auth.WeakPassword",
        "La contraseña debe tener al menos 6 caracteres",
        ErrorType.Validation);
    
    public static readonly Error InvalidEmail = new(
        "Auth.InvalidEmail",
        "El formato del email no es válido",
        ErrorType.Validation);
}