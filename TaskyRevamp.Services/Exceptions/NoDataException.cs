namespace TaskyRevamp.Services.Exceptions;
using Microsoft.AspNetCore.Http;

public class NoDataException : ApplicationException
{
    public NoDataException(string message) : base("No Data", message, StatusCodes.Status204NoContent)
    {
    }
}