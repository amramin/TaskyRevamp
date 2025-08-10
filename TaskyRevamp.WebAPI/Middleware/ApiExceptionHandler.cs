using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;

namespace TaskyRevamp.WebAPI.Middleware;

public class ApiExceptionHandler : IExceptionHandler
{
    private readonly IStringLocalizer<ApiExceptionHandler> _localizer;
    public ApiExceptionHandler(IStringLocalizer<ApiExceptionHandler> localizer)
    {
        _localizer = localizer;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken token)
    {
        var message = _localizer["UnexpectedError"];
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = message });
        return true;
    }
}

