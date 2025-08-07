using Microsoft.AspNetCore.Http;

namespace TaskyRevamp.Services;

public static class HttpContextAccessorExtensions
{
    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
    }
    public static string GetUserNameEnglish(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameEnglish")?.Value;
    }
    public static string GetUserNameArabic(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "NameArabic")?.Value;
    }
    public static string GetUserEmail(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;
    }
}