using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Client.Consumer
{
	public class DepartmentConsumer
	{
		private readonly TaskyService _taskyService;

		public DepartmentConsumer(TaskyService taskyService)
		{
			_taskyService = taskyService;
		}

		public async Task<CommonApiResponse<List<DepartmentDto>>> GetDepartments()
		{
			var url = $"api/Department/GetAllDepartments";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<DepartmentDto>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<DepartmentDto>> GetDepartmentById(Guid id)
		{
			var url = $"api/Department/GetDepartmentById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<DepartmentDto>>(url);
			return res;
		}

		public async Task<CommonApiResponse<bool>>AddDepartment(DepartmentDto DepartmentDto)
		{
			var url = $"api/Department/CreateDepartment";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, DepartmentDto);

			return res.Data;
		}

		public async Task<CommonApiResponse<bool>>UpdateDepartment(DepartmentDto DepartmentDto)
		{
			var url = $"api/Department/UpdateDepartment";
			var res = await _taskyService.PostJsonAsync<CommonApiResponse<bool>>(url, DepartmentDto);

			return res.Data;
		}

	
		public async Task<CommonApiResponse<bool>> DeleteDepartment(Guid id)
		{
			var url = $"api/Department/{id}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

			return res;
		}
	}
}
