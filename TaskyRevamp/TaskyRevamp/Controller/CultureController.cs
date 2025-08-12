using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TaskyRevamp.Controller;

[Route("[controller]/[action]")]
public class CultureController : ControllerBase
{
    [HttpGet("Set")]
    public IActionResult Set(string culture, string redirectUri)
    {
        // Save culture in a cookie so server knows for future requests
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(redirectUri);
    }
}