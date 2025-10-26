namespace heraguard.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    private readonly T _value;
    
    public T Value => IsSuccess 
        ? _value 
        : throw new InvalidOperationException("No se puede acceder al valor de un resultado fallido");
    
    protected internal Result(T value, bool isSuccess, Error error) 
        : base(isSuccess, error)
    {
        _value = value;
    }
    
    public static Result<T> Success(T value) => new(value, true, Error.None);
    public static new Result<T> Failure(Error error) => new(default, false, error);
}