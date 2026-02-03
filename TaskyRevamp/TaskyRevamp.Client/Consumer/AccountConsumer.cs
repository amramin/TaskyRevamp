using System.Net.Http.Json;
using TaskyRevamp.Client.Services;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Client.Consumer;

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
    public async Task<CommonApiResponse<int>> SyncUsers(Guid LogedInUser)
    {
        var url = $"api/Account/SyncUsers/{LogedInUser}";
        var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<int>>(url);
        return res;

    }
    public async Task<CommonApiResponse<PagedResult<UserDto>>> GetUsers(int pageNumber, int? pageSize)
    {
        var ret = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<UserDto>>>($"api/Account/GetUsers?pageNumber={pageNumber}&pageSize={pageSize}");
        return ret;
    }
}