using Stingray.Components.MultiSelectComponent.Dtos;
using System.Net.Http.Json;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class UserConsumer
	{
		private readonly TaskyService _taskyService;

		public UserConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}
		public async Task<CommonApiResponse<List<UserDto>>> GetUsers()
		{
			var ret = await _taskyService.httpClient.GetFromJsonAsync<CommonApiResponse<List<UserDto>>>($"api/User/GetUsers");
			return ret;
		}
		public async Task<CommonApiResponse<DepartmentDto>> GetUsersByDepartment(Guid DepartmentId)
		{
			var ret = await _taskyService.GetFromJsonAsync<CommonApiResponse<DepartmentDto>>($"api/User/GetUsersByDepartment/{DepartmentId.ToString()}");

			return ret;
		}
		public async Task<CommonApiResponse<List<UserDto>>> GetAllUNassignedUsers()
		{
			var url = $"api/User/GetAllUsers";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<UserDto>>>(url);

			return res;
		}
		public async Task<CommonApiResponse<SearchableDropDownDto<DdlDto>>> GetUsersBySearchValue(string searchValue, int pageSize, int offset)
		{
			var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			var ret = await _taskyService.GetFromJsonAsync<CommonApiResponse<SearchableDropDownDto<DdlDto>>>($"api/User/GetUsers/{culture}/{pageSize}/{offset}?SearchValue={searchValue}");
			return ret;
		}
		public async Task<CommonApiResponse<UserDto>> GetUserById(Guid id)
		{
			var url = $"api/User/GetUserById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<UserDto>>(url);
			return res;
		}
		public async Task<CommonApiResponse<DdlDto>> GetSelectedUserDdlById(Guid id)
		{
			var ret = await _taskyService.httpClient.GetFromJsonAsync<CommonApiResponse<DdlDto>>($"api/User/GetSelectedUserDdlById/{id}");
			return ret;
		}
		public async Task<CommonApiResponse<bool>> CreateAssignedUser(AssignedUserDto UserDto)
		{
			var url = $"api/User/CreateAssignedUser";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, UserDto);

			return res.Data;
		}

		public async Task<CommonApiResponse<bool>> UpdateUser(UserDto UserDto)
		{
			var url = $"api/User/UpdateUser";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, UserDto);

			return res.Data;
		}


		public async Task<CommonApiResponse<bool>> DeleteUser(Guid id)
		{
			var url = $"api/User/{id}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

			return res;
		}
	}
}
