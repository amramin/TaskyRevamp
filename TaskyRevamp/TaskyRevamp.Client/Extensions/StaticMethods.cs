using System.Security.Claims;
using System.Text.Json;

namespace TaskyRevamp.Client.Extensions;

public static class StaticMethods
{
    public static string GetClaim(this ClaimsIdentity user, string claimType)
    {
        return user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }

    public static Claim GetClaim(this ClaimsPrincipal user, string claimType)
    {
        return user.Claims.FirstOrDefault(c => c.Type == claimType);
    }

    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}