using System.Net.Http.Json;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Client.Pages.Consumer;

public class AccountConsumer
{
    private readonly TaskyService _taskyService;

    public AccountConsumer(TaskyService surveyService)
    {
        _taskyService = surveyService;
    }

    public async Task<CommonApiResponse<string>> Authenticate(UserLoginDto loginDto)
    {
        var url = $"api/Account/Authenticate";
        var ret = await _taskyService.PostJsonAsyncWithJsonConvert<string, UserLoginDto>(url, loginDto, false);
        return ret;
    }
}