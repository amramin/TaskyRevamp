using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Syncfusion.Blazor;
using System.Globalization;
using System.Text.Json;
using TaskyRevamp.Client;
using TaskyRevamp.Client.Consumer;
using TaskyRevamp.Client.Extensions;
using TaskyRevamp.Client.Services;
using TaskyRevamp.Client.SyncfusionLocalization;
using TaskyRevamp.Dto.GeneralDto;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

using var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
using var response = await http.GetAsync("appsettings.json");
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(stream);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddLocalization();
//builder.Services.AddAuthorizationCore();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXtdcHVTRGBeVkBzWkNWYE4=");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "ar-EG" };
    options.SetDefaultCulture("en-US")
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});

builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddSingleton<PopupService>();
builder.Services.AddScoped<TaskyService>();
builder.Services.AddScoped<FileManagementService>();
builder.Services.AddSingleton<ToastService>();
builder.Services.AddSingleton<LoaderService>();


//Consumers
builder.Services.AddTransient<AccountConsumer>();
builder.Services.AddTransient<RecycleBinSettingConsumer>();
builder.Services.AddTransient<RejectionSettingConsumer>();
builder.Services.AddTransient<PrioritySettingConsumer>();
builder.Services.AddTransient<NotificationTypeTemplateConsumer>();
builder.Services.AddTransient<SendEmailConsumer>();
builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));

builder.Services.AddTransient<DepartmentConsumer>();
builder.Services.AddTransient<TaskConsumer>();
builder.Services.AddTransient<UserConsumer>();
builder.Services.AddTransient<UserDelegationConsumer>();

builder.Services.AddTransient<StatusSettingConsumer>();
builder.Services.AddTransient<ViewTaskSettingConsumer>();
builder.Services.AddTransient<DefaultViewSettingConsumer>();
builder.Services.AddTransient<WorkingDaysSettingConsumer>();
builder.Services.AddTransient<SourceSettingConsumer>();
builder.Services.AddTransient<TypeSettingConsumer>();
builder.Services.AddTransient<WeeklyReportSettingConsumer>();
builder.Services.AddTransient<DefaultColumnsSettingConsumer>();
builder.Services.AddTransient<FilterFieldsSettingConsumer>();
builder.Services.AddTransient<AddTaskSettingConsumer>();
builder.Services.AddTransient<GeneralModuleConsumer>();
builder.Services.AddTransient<ReportModuleConsumer>();
builder.Services.AddTransient<PrivilegeConsumer>();
builder.Services.AddTransient<SystemIdentityConsumer>();
builder.Services.AddTransient<TaskCommentConsumer>();
builder.Services.AddTransient<TaskAttachmentConsumer>();
builder.Services.AddTransient<TaskDependencyConsumer>();

var configuration = builder.Configuration;

var apiUrl = configuration.GetValue<string>("TaskyService");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) }).Configure<JsonSerializerOptions>(options =>
{
    options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});


builder.Services.Configure<PaginationSettings>(
    builder.Configuration.GetSection("Pagination"));
//builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

var host = builder.Build();
const string defaultCulture = "en-US";
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