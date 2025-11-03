namespace heraguard.Domain.Common.Errors;

public static class ActivityErrors
{
    public static readonly Error NotFound = new(
        "Activity.NotFound",
        "La actividad no fue encontrada",
        ErrorType.NotFound
    );

    public static readonly Error InvalidDoctor = new(
        "Activity.InvalidDoctor",
        "El doctor especificado no existe",
        ErrorType.Conflict
    );

    public static readonly Error InvalidElder = new(
        "Activity.InvalidElder",
        "El adulto mayor especificado no existe",
        ErrorType.Conflict
    );

    public static readonly Error InvalidCaregiver = new(
        "Activity.InvalidCaregiver",
        "El cuidador especificado no existe",
        ErrorType.Conflict
    );
}