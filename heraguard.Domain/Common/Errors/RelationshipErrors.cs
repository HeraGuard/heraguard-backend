namespace heraguard.Domain.Common.Errors;

public class RelationshipErrors
{
    public static readonly Error NotFound = new(
        "Relationship.NotFound", 
        "La relacion no fue encontrada", 
        ErrorType.NotFound
    );
    
    public static readonly Error AlreadyExists = new(
        "Relationship.AlreadyExists", 
        "La relacion ya existe", 
        ErrorType.Conflict
    );
}