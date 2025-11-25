namespace heraguard.Domain.Common.Errors;

public static class ScheduleErrors
{
    public static readonly Error NotFound = new(
        "Schedule.NotFound", 
        "El horario no fue encontrado", 
        ErrorType.NotFound
    );
    
    public static readonly Error AlreadyExists = new(
        "Schedule.AlreadyExists", 
        "El horario ya existe", 
        ErrorType.Conflict
    );
}