using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json;
using TaskyRevamp.Client;
using TaskyRevamp.Client.Extensions;
using TaskyRevamp.Client.Pages.Consumer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "ar-EG" };
    options.SetDefaultCulture("ar-EG")
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});



builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddScoped<PopupService>();
builder.Services.AddScoped<TaskyService>();
//Consumers
builder.Services.AddTransient<AccountConsumer>();

var configuration = builder.Configuration;
var apiUrl = configuration.GetValue<string>("TaskyService");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) }).Configure<JsonSerializerOptions>(options =>
{
    options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

var host = builder.Build();

const string defaultCulture = "ar-EG";
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");
var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);
if (result == null)
{
    await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();