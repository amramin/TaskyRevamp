using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace TaskyRevamp.WebAPI.Pipeline;

public class ExtractCustomHeaderAttribute : ActionFilterAttribute
{
    private const string HeaderKeyName = "BlazorCulture";
    private const string DefaultCulture = "en-US"; // Fallback culture

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue))
        {
            string cultureName = headerValue.ToString();
            context.HttpContext.Items[HeaderKeyName] = $"{cultureName}-received";

            try
            {
                // Attempt to set the culture specified in the header
                var cultureInfo = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch (CultureNotFoundException)
            {
                // Fallback to default culture if the specified culture is invalid
                var fallbackCulture = new CultureInfo(DefaultCulture);
                Thread.CurrentThread.CurrentCulture = fallbackCulture;
                Thread.CurrentThread.CurrentUICulture = fallbackCulture;
            }
        }
        else
        {
            context.HttpContext.Items[HeaderKeyName] = $"{DefaultCulture}-received";
            // Set the default culture if the header is not present
            var fallbackCulture = new CultureInfo(DefaultCulture);
            Thread.CurrentThread.CurrentCulture = fallbackCulture;
            Thread.CurrentThread.CurrentUICulture = fallbackCulture;
        }
    }
}
