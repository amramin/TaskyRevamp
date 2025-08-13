using System.Net.Http.Json;
using TaskyRevamp.Client.Services;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Client.Pages.Consumer;

public class AccountConsumer
{
    private readonly TaskyService _taskyService;
    private readonly LoaderService _loader;
    public AccountConsumer(TaskyService surveyService, LoaderService loader)
    {
        _taskyService = surveyService;
        _loader = loader;
    }

    public async Task<CommonApiResponse<string>> Authenticate(UserLoginDto loginDto)
    {
        var url = $"api/Account/Authenticate";
        try
        {
            _loader.Show();
            var ret = await _taskyService.PostJsonAsyncWithJsonConvert<string, UserLoginDto>(url, loginDto, false);
            return ret;
        }
        finally { _loader.Hide(); }

    }
}