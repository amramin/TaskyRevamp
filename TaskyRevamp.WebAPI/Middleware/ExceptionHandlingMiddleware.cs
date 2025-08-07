using System.Text.Json;
using TaskyRevamp.Domain.Exceptions;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.WebAPI.Exeptions;
using ApplicationException = TaskyRevamp.WebAPI.Exeptions.ApplicationException;

namespace TaskyRevamp.WebAPI.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new ApiErrorDto
        {
            Title = GetTitle(exception),
            Status = statusCode,
            Detail = exception.Message,
            Errors = GetErrors(exception)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            NoDataException => StatusCodes.Status204NoContent,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitle(Exception exception)
    {
        return exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };
    }

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;
        if (exception is ValidationException validationException) errors = validationException.ErrorsDictionary;
        return errors;
    }
}