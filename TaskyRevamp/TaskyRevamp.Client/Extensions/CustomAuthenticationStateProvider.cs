using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace TaskyRevamp.Client.Extensions;


//public class CustomAuthenticationStateProvider : AuthenticationStateProvider
//{
//    private AuthenticationState authenticationState;

//    public CustomAuthenticationStateProvider(CustomAuthenticationService service)
//    {
//        authenticationState = new AuthenticationState(service.CurrentUser);

//        service.UserChanged += (newUser) =>
//        {
//            authenticationState = new AuthenticationState(newUser);
//            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
//        };
//    }

//    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
//        Task.FromResult(authenticationState);
//}


public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Get token from LocalStorage (you may also use SessionStorage based on your case)
        var token = await _localStorageService.GetItemAsync<string>("bearerToken");

        var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(token), "Bearer");

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    // Method to mark the user as logged in
    public void MarkUserAsAuthenticated(string token)
    {
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "Bearer");
        var user = new ClaimsPrincipal(identity);
        var authState = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    // Method to mark the user as logged out
    public void MarkUserAsLoggedOut()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = new AuthenticationState(anonymous);

        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        // Replace WebEncoders with a WASM-compatible approach
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

}
