namespace TaskyRevamp.Client;
using Microsoft.JSInterop;
using System.Globalization;

public class LanguageService
{
    private readonly IJSRuntime _js;
    public event Action? OnLanguageChanged;

    public LanguageService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<string> GetLanguageAsync()
    {
        var lang = await _js.InvokeAsync<string>("localStorage.getItem", "lang");
        return string.IsNullOrEmpty(lang) ? "en" : lang;
    }

    public async Task SetLanguageAsync(string lang)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", "lang", lang);
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(lang);
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(lang);
        OnLanguageChanged?.Invoke();
    }
}
