using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using TaskyRevamp.Dto.GeneralDto;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TaskyRevamp.Client.Extensions;


public static class HttpClientExtenstions
{
    public static async Task<CommonApiResponse<T>> GetJsonAsync<T>(this HttpClient http, string url)
    {
        try
        {
            if (url[0] == '/') url = url.Substring(1);

            var httpResponse = await http.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonSerializer.Deserialize<CommonApiResponse<T>>(
                    responseString.Replace("undefined", "null"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return content;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<CommonApiResponse<ApiErrorDto>>(responseString,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return CommonApiResponse<T>.CreateError(error.ErrorMessage, error?.Errors ?? null);
            }
        }
        catch (Exception ex)
        {
            return CommonApiResponse<T>.CreateError($"Error Calling Api: {ex.Message}");
        }
    }

    public static async Task<CommonApiResponse<T>> GetJsonAsyncWithJsonConvert<T>(this HttpClient http, string url)
        where T : class
    {
        try
        {
            if (url[0] == '/') url = url.Substring(1);

            var httpResponse = await http.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content =
                    JsonConvert.DeserializeObject<CommonApiResponse<T>>(
                        responseString.Replace("undefined", "null"));

                return content;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<CommonApiResponse<ApiErrorDto>>(responseString);
                return CommonApiResponse<T>.CreateError(error.Data.Detail, error?.Errors ?? null);
            }
        }
        catch (Exception ex)
        {
            return CommonApiResponse<T>.CreateError($"Error Calling Api: {ex.Message}");
        }
    }

    public static async Task<CommonApiResponse<TReturn>> PostJsonAsync<TReturn, TValue>(this HttpClient http,
        string url,
        TValue value) where TReturn : class
    {
        try
        {
            if (url[0] == '/') url = url.Substring(1);

            var httpResponse = await http.PostAsJsonAsync(url, value);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonSerializer.Deserialize<CommonApiResponse<TReturn>>(
                    responseString.Replace("undefined", "null"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return content;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<CommonApiResponse<ApiErrorDto>>(responseString,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return CommonApiResponse<TReturn>.CreateError(error.Data.Detail, error?.Errors ?? null);
            }
        }
        catch (Exception ex)
        {
            return CommonApiResponse<TReturn>.CreateError($"Error Calling Api: {ex.Message}");
        }
    }

    public static async Task<CommonApiResponse<TReturn>> PostFileAsync<TReturn>(this HttpClient http,
        string url, MultipartFormDataContent value) where TReturn : class
    {
        try
        {
            if (url[0] == '/') url = url.Substring(1);
            var cancelationToken = new CancellationTokenSource(TimeSpan.FromSeconds(1000));

            var httpResponse = await http.PostAsync(url, value, cancelationToken.Token);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonSerializer.Deserialize<CommonApiResponse<TReturn>>(
                    responseString.Replace("undefined", "null"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return content;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<CommonApiResponse<ApiErrorDto>>(responseString,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return CommonApiResponse<TReturn>.CreateError(error.Data.Detail, error?.Errors ?? null);
            }
        }
        catch (Exception ex)
        {
            return CommonApiResponse<TReturn>.CreateError($"Error Calling Api: {ex.Message}");
        }
    }

    public static async Task<CommonApiResponse<TReturn>> PostJsonAsync<TReturn>(this HttpClient http,
        string url,
        object value)
    {
        try
        {
            if (url[0] == '/') url = url.Substring(1);
            var jsonValue = JsonConvert.SerializeObject(value);
            var x = new StringContent(jsonValue, Encoding.UTF8, "application/json");
            var httpResponse = await http.PostAsync(url, x);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonSerializer.Deserialize<CommonApiResponse<TReturn>>(
                    responseString.Replace("undefined", "null"),
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return content;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<CommonApiResponse<ApiErrorDto>>(responseString,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return CommonApiResponse<TReturn>.CreateError(error?.ErrorMessage?? "", error?.Errors?? null);
            }
        }
        catch (Exception ex)
        {
            return CommonApiResponse<TReturn>.CreateError($"Error Calling Api: {ex.Message}");
        }
    }


    public static async Task<CommonApiResponse<TReturn>> PostJsonAsyncWithJsonConvert<TReturn, TValue>(
        this HttpClient http,
        string url,
        TValue value)
    {
        try
        {
            if (url[0] == '/') url = url.Substring(1);

            var httpResponse = await http.PostAsJsonAsync(url, value);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<CommonApiResponse<TReturn>>(
                    responseString.Replace("undefined", "null"));
                return content;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<CommonApiResponse<ApiErrorDto>>(responseString);
                try
                {
                    return CommonApiResponse<TReturn>.CreateError(error.Data.Detail, error?.Errors ?? null);
                }
                catch
                {
                    return CommonApiResponse<TReturn>.CreateError(error.ErrorMessage);
                }
            }
        }
        catch (Exception ex)
        {
            return CommonApiResponse<TReturn>.CreateError($"Error Calling Api: {ex.Message}");
        }
    }
}