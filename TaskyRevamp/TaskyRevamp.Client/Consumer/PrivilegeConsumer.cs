using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Client.Consumer
{
    public class PrivilegeConsumer
    {
        private readonly TaskyService _taskyService;

        public PrivilegeConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }
		public async Task<CommonApiResponse<PagedResult<PrivilegeDtoWithName>>> GetPrivileges(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldPrivileg> searchFields = null, string searchText = null)
		{
			var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);

			var url = $"api/Privilege/GetPrivilleges{queryString}";

			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<PrivilegeDtoWithName>>>(url);

			return res;
		}

		public async Task<CommonApiResponse<PrivilegeDto>> GetPrivilegeById(Guid id)
		{
			var url = $"api/Privilege/GetPrivilegeById/{id}";
			var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PrivilegeDto>>(url);
			return res;
		}
		public async Task<CommonApiResponse<bool>> CreatePrivilege(PrivilegeDto privilegeDto)
        {
            var url = $"api/Privilege/CreatePrivilege";
            var res = await _taskyService.PostJsonAsync<bool>(url, privilegeDto);

            return res;
        }

        public async Task<CommonApiResponse<bool>> UpdatePrivilege(PrivilegeDto privilegeDto)
        {
            var url = $"api/Privilege/UpdatePrivilege";
            var res = await _taskyService.PostJsonAsync<bool>(url, privilegeDto);

            return res;
        }
		public async Task<CommonApiResponse<bool>> DeletePrivilege(Guid id)
		{
			var url = $"api/Privilege/DeletePrivilege/{id}";
			var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

			return res;
		}
	}
}
