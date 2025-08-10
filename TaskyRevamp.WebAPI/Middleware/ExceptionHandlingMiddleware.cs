using Microsoft.Extensions.Localization;
using System.Text.Json;
using TaskyRevamp.Domain.Exceptions;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.WebAPI.Exeptions;
using ApplicationException = TaskyRevamp.WebAPI.Exeptions.ApplicationException;

namespace TaskyRevamp.WebAPI.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _logger = logger;
        _stringLocalizer = stringLocalizer;
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

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = CommonApiResponse<Boolean>.Create(statusCode, false, GetTitle(exception), exception.Message, GetErrors(exception));


        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private int GetStatusCode(Exception exception)
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

    private string GetTitle(Exception exception)
    {
        var error = _stringLocalizer[exception.Message];
        return error;


        //return exception switch
        //{
        //    ApplicationException applicationException => applicationException.Title,
        //    _ => "Server Error"
        //};
    }

    private IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;
        if (exception is ValidationException validationException) errors = validationException.ErrorsDictionary;
        return errors;
    }
}