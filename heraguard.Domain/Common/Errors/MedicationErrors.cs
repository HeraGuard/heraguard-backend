namespace heraguard.Domain.Common.Errors;

public static class MedicationErrors
{
    public static readonly Error NotFound = new(
        "Medication.NotFound", 
        "La medicación no fue encontrada", 
        ErrorType.NotFound
    );
    
    public static readonly Error InvalidDoctor = new(
        "Medication.InvalidDoctor", 
        "El doctor especificado no existe", 
        ErrorType.Conflict
    );
    
    public static readonly Error InvalidElder = new(
        "Medication.InvalidElder", 
        "El adulto mayor especificado no existe", 
        ErrorType.Conflict
    );
    
    public static readonly Error InvalidCaregiver = new(
        "Medication.InvalidCaregiver", 
        "El cuidador especificado no existe", 
        ErrorType.Conflict
    );
}