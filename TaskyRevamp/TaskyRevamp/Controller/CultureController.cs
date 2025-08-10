using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TaskyRevamp.Controller;

[Route("[controller]/[action]")]
public class CultureController : ControllerBase
{
    public IActionResult Set(string culture, string redirectUri)    
    {
        if (!string.IsNullOrEmpty(culture))
        {
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture));
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                cookieValue,
                new CookieOptions { IsEssential = true, Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            // Log the culture and cookie value to confirm it's being set
            Console.WriteLine($"Setting culture cookie: {cookieValue}");
        }

        return LocalRedirect(redirectUri);
    }
}