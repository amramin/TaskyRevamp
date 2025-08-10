using System.Net;
using System.Text.Json;

namespace TaskyRevamp.Dto.GeneralDto;

public class CommonApiResponse<T>
{
    public CommonApiResponse()
    {
        Data = default!;
    }

    public static CommonApiResponse<T> Create(int statusCode, T result = default, string errorMessage = null, string details = null, object errors = null)
    {
        //if (statusCode == int.Parse(HttpStatusCode.InternalServerError.ToString()))
        //{
        //    var errorDto = JsonSerializer.Deserialize<ApiErrorDto>(errorMessage,
        //        new JsonSerializerOptions()
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });

        //    return new CommonApiResponse<T>(statusCode, default, errorDto.Detail);
        //}


        return new CommonApiResponse<T>(statusCode, result, errorMessage, details, errors);
    }

    public static CommonApiResponse<T> CreateError(string errorMessage)
    {
        return new CommonApiResponse<T>(errorMessage);
    }

    public string Version => "1.2.3";

    public int? Code { get; set; }
    public string RequestId { get; }

    public string ErrorMessage { get; set; }

    public T? Data { get; set; } = default!;
    public string Detail { get; set; }
    public object Errors { get; set; }

    protected CommonApiResponse(string errorMessage)
    {
        Count = 0;
        RequestId = Guid.NewGuid().ToString();
        Data = Activator.CreateInstance<T>();
        ErrorMessage = errorMessage;
    }

    protected CommonApiResponse(int statusCode, T data = default, string errorMessage = null, string details = null, object errors = null)
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