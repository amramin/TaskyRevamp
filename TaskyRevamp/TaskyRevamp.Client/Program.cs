using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
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


//Consumers
builder.Services.AddTransient<AccountConsumer>();

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