using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using TaskyRevamp.Client.Extensions;
using TaskyRevamp.Client.Services;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client;

public class TaskyService
{
    public HttpClient httpClient;

    public IJSRuntime _js;
    private string bearerToken;
    public string bearerCulture;
    public Guid currentUserId;
    public IJSRuntime JS;
    private readonly ILocalStorageService _localStorage;
    private NavigationManager NavigationManager;
    private readonly LoaderService _loader;

    public TaskyService(HttpClient httpClient, IJSRuntime js, ILocalStorageService localStorage, NavigationManager navigationManager, LoaderService loader)
    {
        this.httpClient = httpClient;

        JS = js;
        _localStorage = localStorage;
        NavigationManager = navigationManager;
        _loader = loader;
    }

    private readonly string[] _validImageExtensions = [".jpg", ".jpeg", ".png"];
    private readonly string[] _validVideoExtensions = [".mp4", ".avi", ".mov", ".mkv", ".webm", ".flv", ".wmv", ".mpeg", ".mpg"];

    public void NavigateToLogin()
    {
        var returnUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        var loginUrl = string.IsNullOrEmpty(returnUrl) ? "/login" : $"/login?returnUrl={returnUrl}";

        ClearLocalStorage();

        NavigationManager.NavigateTo(loginUrl, true);
    }

    public void ClearLocalStorage()
    {
        _localStorage.RemoveItemAsync("bearerToken");
        _localStorage.RemoveItemAsync("NameEnglish");
        _localStorage.RemoveItemAsync("NameArabic");
        _localStorage.RemoveItemAsync("Username");
        _localStorage.RemoveItemAsync("Email");
        _localStorage.RemoveItemAsync("Id");
        _localStorage.RemoveItemAsync("DelegatedUsersId");
    }

    public string PreparePaginatedSearchQueryString<T>(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<T> searchFields = null, string searchText = null)
    {
        var query = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"pageSize={pageSize}",
                $"sortByColumnName={Uri.EscapeDataString(sortByColumnName)}",
                $"sortAscending={sortAscending}"
            };

        if (searchFields != null && searchFields.Count > 0)
        {
            foreach (var field in searchFields)
            {
                query.Add($"searchFields={field.ToString()}");
            }
        }

        if (!string.IsNullOrWhiteSpace(searchText))
            query.Add($"searchText={searchText}");

        var queryString = "?" + string.Join("&", query);

        return queryString;
    }

    public string GetMimeType(byte[] bytes)
    {
        if (bytes.Length < 4) return null;

        // PNG
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return "image/png";

        // JPG
        if (bytes[0] == 0xFF && bytes[1] == 0xD8)
            return "image/jpeg";

        // SVG (starts with "<svg")
        if (Encoding.UTF8.GetString(bytes.Take(4).ToArray()).Contains("<svg"))
            return "image/svg+xml";

        return null;
    }

    public async Task<SystemIdentityDto> GetSystemIdentity()
    {
        var systemIdentity = new SystemIdentityDto();
        var url = $"api/SystemIdentity/GetSystemIdentitySetting";
        var res = await GetFromJsonAsync<CommonApiResponse<SystemIdentityDto>>(url);

        if (res.Success)
        {
            systemIdentity = res.Data;
        }

        return systemIdentity;
    }

    public string GetSystemLogoAsImgSrc(byte[] logo)
    {
        var systemLogoBase64 = "";
        var mimeType = GetMimeType(logo);
        systemLogoBase64 = $"data:{mimeType};base64,{Convert.ToBase64String(logo)}";

        return systemLogoBase64;
    }

	public event Action<SystemIdentityDto> OnSystemIdentityChanged;
	public void NotifySystemIdentityChanged(SystemIdentityDto identity)
	{
		OnSystemIdentityChanged?.Invoke(identity);
	}
	public async Task ChangeTheme(SystemIdentityDto systemIdentity)
    {
        await JS.InvokeVoidAsync("setThemeColor", "--primary-color", systemIdentity.PrimaryColor);
        await JS.InvokeVoidAsync("setThemeColor", "--secondary-color", systemIdentity.PrimaryColor);

        await JS.InvokeVoidAsync("setThemeColor", "--active-primary", systemIdentity.PrimaryActiveColor);
    
        await JS.InvokeVoidAsync("setThemeColor", "--light-200", systemIdentity.NavigationBackground);
        await JS.InvokeVoidAsync("setThemeColor", "--light-300", systemIdentity.BorderColor);

        await JS.InvokeVoidAsync("setThemeColor", "--dark-900", systemIdentity.MainTitle);
        await JS.InvokeVoidAsync("setThemeColor", "--dark-800", systemIdentity.SubTitle);

	}

    private async Task<bool> CheckForToken()
    {
        try
        {

            bearerCulture = await _localStorage.GetItemAsStringAsync("BlazorCulture");

            if (!httpClient.DefaultRequestHeaders.TryGetValues("BlazorCulture", out var values)
                || !values.Contains(bearerCulture))
            {
                httpClient.DefaultRequestHeaders.Remove("BlazorCulture");
                httpClient.DefaultRequestHeaders.Add("BlazorCulture", bearerCulture);
            }


            bearerToken = await _localStorage.GetItemAsStringAsync("bearerToken");


            if (IsTokenExpired(bearerToken))
            {
                NavigateToLogin();
                return false;
            }

            httpClient.DefaultRequestHeaders.Accept.Clear();
            //   Console.WriteLine($"BearerToken localstoreage: {bearerToken}");
            httpClient.Timeout = TimeSpan.FromSeconds(1000);


            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", bearerToken);


            return true;
        }
        catch (Exception e)
        {
            return true;
            Console.WriteLine($"Error in CheckForToken: {e.Message}");
            NavigateToLogin();
            return false;
        }
    }

    public bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
        var tokenTicks = long.Parse(tokenExp);

        var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

        var now = DateTime.UtcNow.ToUniversalTime();

        var valid = tokenDate >= now;

        return !valid;
    }

    public async Task<CommonApiResponse<T>> PostJsonAsyncWithJsonConvert<T, T1>(string url, T1 loginDto, bool checkForToken = true)

    {
        try
        {
            _loader.Show();
            if (checkForToken)
                if (!await CheckForToken())
                    return new CommonApiResponse<T>();

            var response = await httpClient.PostJsonAsyncWithJsonConvert<T, T1>(url, loginDto);
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<CommonApiResponse<T>> PostJsonAsync<T, T1>(string url, T1 componenetFilters) where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new CommonApiResponse<T>();

            var response = await httpClient.PostJsonAsync<T, T1>(url, componenetFilters);
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<CommonApiResponse<T>> PostFileAsync<T>(string url, MultipartFormDataContent value)
        where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new CommonApiResponse<T>();

            var response = await httpClient.PostFileAsync<T>(url, value);
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<CommonApiResponse<T>> PostJsonAsync<T>(string url, object value)
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new CommonApiResponse<T>();

            var response = await httpClient.PostJsonAsync<T>(url, value);
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<CommonApiResponse<T>> GetJsonAsync<T>(string apiWavestatusGetwavestatuslist)
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new CommonApiResponse<T>();

            var response = await httpClient.GetJsonAsync<T>(apiWavestatusGetwavestatuslist);
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T componenetFilters) where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new HttpResponseMessage();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(componenetFilters, settings);
            var response = await httpClient.PostAsync(url, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            return response;

        }
        finally { _loader.Hide(); }
    }

    public async Task<T> GetFromJsonAsync<T>(string url) where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return await Task.FromResult<T>(null);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var response = await httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<T>(response, settings);
            return result;
        }
        finally { _loader.Hide(); }
    }

    public async Task<T> DeleteFromJsonAsync<T>(string url) where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return await Task.FromResult<T>(null);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var response = await httpClient.DeleteAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseString, settings);
            return result;
        }
        finally { _loader.Hide(); }
    }
    public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T value) where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new HttpResponseMessage();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(value, settings);
            var response = await httpClient.PutAsync(url, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<CommonApiResponse<T>> GetJsonAsyncWithJsonConvert<T>(string url) where T : class
    {
        try
        {
            _loader.Show();
            if (!await CheckForToken())
                return new CommonApiResponse<T>();

            var response = await httpClient.GetJsonAsyncWithJsonConvert<T>(url);
            return response;
        }
        finally { _loader.Hide(); }
    }

    public async Task<string?> GetStringAsync(string url)
    {
        try
        {
            _loader.Show();

            if (!await CheckForToken())
                return "";

            var response = await httpClient.GetStringAsync(url);
            return response;
        }
        finally { _loader.Hide(); }
    }


}