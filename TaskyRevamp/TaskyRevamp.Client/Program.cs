using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskyRevamp.Client;
using TaskyRevamp.Client.Extensions;
using TaskyRevamp.Client.Pages.Consumer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//Consumers
builder.Services.AddTransient<AccountConsumer>();

await builder.Build().RunAsync();
