namespace TaskyRevamp.WebAPI.Pipeline;

public class ExtractCustomHeaderMiddleware
{
    private readonly RequestDelegate _next;
    public ExtractCustomHeaderMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context)
    {

        await _next(context);
    }
}