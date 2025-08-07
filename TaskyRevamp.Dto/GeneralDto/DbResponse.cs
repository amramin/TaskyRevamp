namespace TaskyRevamp.Dto.GeneralDto;

 

public class DbResponse<T> : DbResponse
{
    protected internal DbResponse(T? value, bool success, string error)
        : base(success, error)
    {
        Value = value;
    }

    public T? Value { get; set; }
}

public class DbResponse
{
    protected DbResponse(bool success, string error)
    {
        if (success && error != string.Empty)
            throw new InvalidOperationException();
        if (!success && error == string.Empty)
            throw new InvalidOperationException();
        Success = success;
        Error = error;
    }

    public bool Success { get; }
    public string Error { get; }
    public bool IsFailure => !Success;

    public static DbResponse Fail(string message)
    {
        return new DbResponse(false, message);
    }

    public static DbResponse<T> Fail<T>(string message)
    {
        return new DbResponse<T>(default, false, message);
    }

    public static DbResponse Ok()
    {
        return new DbResponse(true, string.Empty);
    }

    public static DbResponse<T> Ok<T>(T? value)
    {
        return new DbResponse<T>(value, true, string.Empty);
    }
}