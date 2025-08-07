using System.Net;
using System.Text.Json;

namespace TaskyRevamp.Dto.GeneralDto;

public class CommonApiResponse<T> 
{
    public CommonApiResponse()
    {
    }

    public static CommonApiResponse<T> Create(HttpStatusCode statusCode, T result = default, string errorMessage = null)
    {
        if (statusCode == HttpStatusCode.InternalServerError)
        {
            var errorDto = JsonSerializer.Deserialize<ApiErrorDto>(errorMessage,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            return new CommonApiResponse<T>(statusCode, default, errorDto.Detail);
        }


        return new CommonApiResponse<T>(statusCode, result, null);
    }

    public static CommonApiResponse<T> CreateError(string errorMessage)
    {
        return new CommonApiResponse<T>(errorMessage);
    }

    public string Version => "1.2.3";

    public int? Code { get; set; }
    public string RequestId { get; }

    public string ErrorMessage { get; set; }

    public T Data { get; set; }

    protected CommonApiResponse(string errorMessage)
    {
        Count = 0;
        RequestId = Guid.NewGuid().ToString();
        Data = default;
        ErrorMessage = errorMessage;
    }

    protected CommonApiResponse(HttpStatusCode statusCode, T data = default, string errorMessage = null)
    {
        Count = 0;
        RequestId = Guid.NewGuid().ToString();
        Code = null;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public int Count { get; set; }

    public bool Success => string.IsNullOrWhiteSpace(ErrorMessage);

    public int? ListData { get; set; } = null;
    public int Sum { get; set; } = 0;
}