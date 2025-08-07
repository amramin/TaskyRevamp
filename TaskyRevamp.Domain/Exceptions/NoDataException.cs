namespace TaskyRevamp.Domain.Exceptions;

public class NoDataException : Exception
{
    public NoDataException(string message) : base(message)
    {
    }
}
public class CannotCreateException : Exception
{
    public CannotCreateException(string message) : base(message)
    {
    }
}
public class DataNotValidException : Exception
{
    public DataNotValidException()
    {
        
    }
    public DataNotValidException(string message) : base(message)
    {
    }
}
