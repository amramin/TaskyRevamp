using Microsoft.Extensions.Localization;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace TaskyRevamp.WebAPI;

public class LocalizedExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IStringLocalizer<LocalizedExceptionMiddleware> _localizer;
    private readonly ILogger<LocalizedExceptionMiddleware> _logger;

    public LocalizedExceptionMiddleware(
        RequestDelegate next,
        IStringLocalizer<LocalizedExceptionMiddleware> localizer,
        ILogger<LocalizedExceptionMiddleware> logger)
    {
        _next = next;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException vex)
        {
            // Validation errors from FluentValidation
            var errors = vex.Errors.Select(e => e.ErrorMessage).ToArray();
            await WriteErrorResponseAsync(context, errors, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            string errorMessage = ex switch
            {
                InvalidOperationException => _localizer["InvalidOperation"],
                ArgumentException => _localizer["InvalidArgument"],
                KeyNotFoundException => _localizer["NotFound"],
                _ => _localizer["UnexpectedError"]
            };

            await WriteErrorResponseAsync(context, new[] { errorMessage }, HttpStatusCode.BadRequest);
        }
    }

    private async Task WriteErrorResponseAsync(HttpContext context, string[] errors, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new { errors };
        var json = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(json);
    }
}
