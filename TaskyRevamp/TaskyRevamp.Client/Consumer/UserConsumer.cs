using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;

namespace TaskyRevamp.Client.Consumer
{
	public class UserConsumer
	{
		private readonly TaskyService _taskyService;

		public UserConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

        public async Task<CommonApiResponse<DepartmentDto>> GetAllUsersByDepartment(Guid DepartmentId)
        {
            var ret = await _taskyService.GetFromJsonAsync<CommonApiResponse<DepartmentDto>>($"api/User/GetAllUsersByDepartment/{DepartmentId.ToString()}");

            return ret;
        }
        public async Task<CommonApiResponse<List<UserDto>>> GetAllUNassignedUsers()
		{
			var url = $"api/User/GetAllUsers";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<UserDto>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<UserDto>> GetUserById(Guid id)
		{
			var url = $"api/User/GetUserById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<UserDto>>(url);
			return res;
		}

		public async Task<CommonApiResponse<bool>> CreateAssignedUser(AssignedUserDto UserDto)
		{
			var url = $"api/User/CreateAssignedUser";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, UserDto);

			return res.Data;
		}

		public async Task<CommonApiResponse<bool>>UpdateUser(UserDto UserDto)
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
