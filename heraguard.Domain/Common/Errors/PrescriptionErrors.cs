namespace heraguard.Domain.Common.Errors;

public static class PrescriptionErrors
{
    public static readonly Error NotFound = new(
        "Prescription.NotFound", 
        "La receta no fue encontrada", 
        ErrorType.NotFound
    );
    
    public static readonly Error InvalidDoctor = new(
        "Prescription.InvalidDoctor", 
        "El doctor especificado no existe", 
        ErrorType.Conflict
    );
    
    public static readonly Error InvalidElder = new(
        "Prescription.InvalidElder", 
        "El adulto mayor especificado no existe", 
        ErrorType.Conflict
    );
    
    public static readonly Error InvalidCaregiver = new(
        "Prescription.InvalidCaregiver", 
        "El cuidador especificado no existe", 
        ErrorType.Conflict
    );
}