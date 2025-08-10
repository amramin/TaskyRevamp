using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using TaskyRevamp.Client;
using TaskyRevamp.Client.Extensions;
using TaskyRevamp.Client.Pages.Consumer;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


//If you want browser-based detection:
//var js = builder.Services.BuildServiceProvider().GetRequiredService<IJSRuntime>();
//var lang = await js.InvokeAsync<string>("navigator.language");
//client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(lang);

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

    // Example: set from browser's culture
    var culture = CultureInfo.CurrentUICulture.Name; // e.g., "ar", "en-US"
    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(culture);

    return client;
});



builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<IJSRuntime>();

//Consumers
builder.Services.AddTransient<AccountConsumer>();

await builder.Build().RunAsync();
