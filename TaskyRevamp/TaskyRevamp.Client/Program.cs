using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json;
using TaskyRevamp.Client;
using TaskyRevamp.Client.Extensions;
using TaskyRevamp.Client.Services;
using TaskyRevamp.Dto.GeneralDto;
using Microsoft.Extensions.Options;
using TaskyRevamp.Client.Consumer;
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
builder.Services.AddSingleton<PopupService>();
builder.Services.AddScoped<TaskyService>();
builder.Services.AddSingleton<ToastService>();
builder.Services.AddSingleton<LoaderService>();


//Consumers
builder.Services.AddTransient<AccountConsumer>();
builder.Services.AddTransient<RecycleBinSettingConsumer>();
builder.Services.AddTransient<RejectionSettingConsumer>();
builder.Services.AddTransient<PrioritySettingConsumer>();
builder.Services.AddTransient<DepartmentConsumer>();

builder.Services.AddTransient<StatusSettingConsumer>();

var configuration = builder.Configuration;

var apiUrl = configuration.GetValue<string>("TaskyService");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) }).Configure<JsonSerializerOptions>(options =>
{
    options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});


builder.Services.Configure<PaginationSettings>(
    builder.Configuration.GetSection("Pagination"));

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