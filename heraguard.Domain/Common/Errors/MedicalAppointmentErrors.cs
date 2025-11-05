namespace heraguard.Domain.Common.Errors;

public static class MedicalAppointmentErrors
{
    public static readonly Error NotFound = new(
        "MedicalAppointment Not Found",
        "La cita médica no se ha encontrado",
        ErrorType.NotFound
    );

    public static readonly Error InvalidDate = new(
        "Invalid Appointment Date",
        "La fecha de la cita médica es inválida o está en el pasado",
        ErrorType.Validation
    );

    public static readonly Error DoctorUnavailable = new(
        "Doctor Unavailable",
        "El doctor no está disponible en la fecha y hora seleccionadas",
        ErrorType.Conflict
    );

    public static readonly Error CaregiverUnavailable = new(
        "Caregiver Unavailable",
        "El cuidador no está disponible en la fecha y hora seleccionadas",
        ErrorType.Conflict
    );

    public static readonly Error OverlappingAppointment = new(
        "Overlapping Appointment",
        "Existe otra cita médica que se superpone con la fecha y hora seleccionadas",
        ErrorType.Conflict
    );

    public static readonly Error InvalidInput = new(
        "Invalid Input",
        "Los datos proporcionados para la cita médica no son válidos",
        ErrorType.Validation
    );
}

