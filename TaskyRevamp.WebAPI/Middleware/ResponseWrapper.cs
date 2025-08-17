using System.Net;
using Newtonsoft.Json;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.WebAPI.Middleware;

public class ResponseWrapper
{
    private readonly RequestDelegate _next;

    public ResponseWrapper(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var originalBody = context.Response.Body;

        // Skip wrapping for specific paths or status codes
        if (context.Request.Path.Value.Contains("Table/save"))
        {
            await _next(context);
            return;
        }

        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            try
            {
                await _next(context);

                // Skip wrapping for 204 (No Content) or 304 (Not Modified)
                if (context.Response.StatusCode == (int)HttpStatusCode.NoContent ||
                    context.Response.StatusCode == (int)HttpStatusCode.NotModified)
                {
                    return;
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                var readToEnd = await new StreamReader(memoryStream).ReadToEndAsync();

                object? objResult;

                try
                {
                    objResult = JsonConvert.DeserializeObject(readToEnd);
                }
                catch
                {
                    if (bool.TryParse(readToEnd, out bool resultBool))
                    {
                        objResult = resultBool;
                    }
                    else if (double.TryParse(readToEnd, out double resultDouble))
                    {
                        objResult = resultDouble;
                    }
                    else if (int.TryParse(readToEnd, out int resultInt))
                    {
                        objResult = resultInt;
                    }
                    else
                    {
                        objResult = readToEnd;
                    }
                }

                // Reset response headers and body
                context.Response.Body = originalBody;
                context.Response.Headers.Remove("Content-Length");

                //var response = CommonApiResponse<object>.Create((int)context.Response.StatusCode, objResult, readToEnd);

                var statusCode = context.Response.StatusCode;
                CommonApiResponse<object> response;

                if (statusCode >= 400)
                {
                    // Error → put body into ErrorMessage
                    response = CommonApiResponse<object>.Create(statusCode, default, readToEnd);
                }
                else
                {
                    // Success → put parsed object into Data
                    response = CommonApiResponse<object>.Create(statusCode, objResult);
                }

                var jsonResponse = JsonConvert.SerializeObject(response);

                if (readToEnd.TrimStart().StartsWith("{") && readToEnd.Contains("\"version\"") && readToEnd.Contains("\"data\""))
                {
                    // Already wrapped, just return it directly
                    context.Response.Body = originalBody;
                    await context.Response.WriteAsync(readToEnd);
                    return;
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResponse);
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}

public static class ResponseWrapperExtensions
{
    public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ResponseWrapper>();
    }
}