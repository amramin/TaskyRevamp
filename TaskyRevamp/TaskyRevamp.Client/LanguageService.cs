
using Blazored.LocalStorage;
using System.Globalization;
using Microsoft.JSInterop;

namespace TaskyRevamp.Client;
public class LanguageService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IJSRuntime _jsRuntime;

    public LanguageService(ILocalStorageService localStorage, IJSRuntime jsRuntime)
    {
        _localStorage = localStorage;
        _jsRuntime = jsRuntime;
    }

    public async Task SetLanguageAsync(string culture)
    {
        await _localStorage.SetItemAsync("culture", culture);
        CultureInfo.CurrentCulture = new CultureInfo(culture);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Reload the page so Auto mode applies new culture in both Server/WASM
        await _jsRuntime.InvokeVoidAsync("location.reload");
    }

    public async Task<string> GetLanguageAsync()
    {
        return await _localStorage.GetItemAsync<string>("culture") ?? "ar";
    }
}

