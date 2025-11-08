namespace heraguard.Domain.Common.Errors;

public class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound", 
        "El usuario no fue encontrada", 
        ErrorType.NotFound
    );
    
    public static readonly Error InvalidCode = new(
        "User.InvalidCode", 
        "Código de vinculación inválido\n", 
        ErrorType.Validation
    );
}

