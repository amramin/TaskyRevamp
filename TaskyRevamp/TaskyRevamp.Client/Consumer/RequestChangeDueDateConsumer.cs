using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Client.Consumer
{
    public class RequestChangeDueDateConsumer
    {
        private readonly TaskyService _taskyService;
        private readonly FileManagementService _fileManagementService;
        public RequestChangeDueDateConsumer(TaskyService taskyService, FileManagementService fileManagementService)
        {
            _taskyService = taskyService;
            _fileManagementService = fileManagementService;
        }
        public async Task<CommonApiResponse<PagedResult<ChangeEndDateRequestDto>>> GetDueDateRequestsByTaskId(Guid tskid, int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchField> searchFields, string searchText=null)
        {
            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);
            var url = $"api/ChangeEndDateRequest/GetAllChangeEndDateRequestsByTaskId/{tskid}{queryString}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<ChangeEndDateRequestDto>>>(url);
            return res;
        }
        public async Task<CommonApiResponse<PagedResult<ChangeEndDateRequestDto>>> GetDueDateRequests( int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldChangeDueDate> searchFields, string searchText = null)
        {
            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);
            var url = $"api/ChangeEndDateRequest/GetAllChangeEndDateRequests{queryString}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<ChangeEndDateRequestDto>>>(url);
            return res;
        }
        public async Task<CommonApiResponse<bool>> UpdateRequestStatus(Guid RequestId, ChangeRequestStatus Status)
        {
            var url = $"api/ChangeEndDateRequest/UpdateRequestStatus/{RequestId}/{Status}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<bool>>(url);
            return res;
        }
    }
}
