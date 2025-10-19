using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
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
        

      public async Task<CommonApiResponse<List<DepartmentDto>>> GetDepartmentsForDDL()
        {


            var url = $"api/Department/GetDepartmentsForDDL";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<DepartmentDto>>>(url);

            return res;
        }

        public async Task<CommonApiResponse<PagedResult<DepartmentDto>>> GetDepartments(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDepartment> searchFields = null, string searchText = null)
        {

            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);

            var url = $"api/Department/GetAllDepartments{queryString}";

            
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<DepartmentDto>>>(url);

			return res;
		}

        

              public async Task<CommonApiResponse<List<DepartmentDto>>> GetDepartmentsNoPagnation( List<SearchFieldDepartment> searchFields = null, string searchText = null)
        {

            var queryString = _taskyService.PrepareNoPaginatedSearchQueryString(searchFields, searchText);

            var url = $"api/Department/GetDepartmentsNoPagnation{queryString}";


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
