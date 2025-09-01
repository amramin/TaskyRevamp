using System.Net;
using System.Text.Json;

namespace TaskyRevamp.Dto.GeneralDto;

public class CommonApiResponse<T>
{
    public CommonApiResponse()
    {
        Data = default!;
    }

    public static CommonApiResponse<T> Create(int statusCode, T result = default, string errorMessage = null, string details = null, Dictionary<string, List<string>> errors = null)
    {
        List<int> errorStatusCodes = new List<int> { 204, 400, 401, 404, 406, 422, 500 };

        if (errorStatusCodes.Contains(statusCode))
        {
            try
            {
                var errorDto = JsonSerializer.Deserialize<ApiErrorDto>(errorMessage,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                return new CommonApiResponse<T>(statusCode, default, errorDto.Detail, details, errorDto.Errors);
            }
            catch (Exception)
            {
                // If deserialization fails, we can just return the error message as is.
                return new CommonApiResponse<T>(statusCode, default, errorMessage, details, errors);
            }
        }

        //return new CommonApiResponse<T>(statusCode, result, null);
        return new CommonApiResponse<T>(statusCode, result, errorMessage, details, errors);
    }

    public static CommonApiResponse<T> CreateError(string errorMessage, Dictionary<string, List<string>>? errors = null)
    {
        return new CommonApiResponse<T>(errorMessage, errors);
    }

    public string Version => "1.2.3";

    public int? Code { get; set; }
    public string RequestId { get; }

    public string ErrorMessage { get; set; }

    public T? Data { get; set; } = default!;
    public string Detail { get; set; }
    public Dictionary<string, List<string>>? Errors { get; set; }

    protected CommonApiResponse(string errorMessage, Dictionary<string, List<string>>? errors)
    {
        Count = 0;
        RequestId = Guid.NewGuid().ToString();
        if (typeof(T) == typeof(string))
            Data = (T)(object)string.Empty;
        else if (typeof(T).IsValueType)
            Data = Activator.CreateInstance<T>();
        else
            Data = Activator.CreateInstance<T>();
        ErrorMessage = errorMessage;
        Errors = errors;
    }

    protected CommonApiResponse(int statusCode, T data = default, string errorMessage = null, string details = null, Dictionary<string, List<string>> errors = null)
    {
        Count = 0;
        RequestId = Guid.NewGuid().ToString();
        Code = null;
        Data = data;
        ErrorMessage = errorMessage;
        Detail = details;
        Errors = errors;
    }

    public int Count { get; set; }

    public bool Success => string.IsNullOrWhiteSpace(ErrorMessage);

    public int? ListData { get; set; } = null;
    public int Sum { get; set; } = 0;
}