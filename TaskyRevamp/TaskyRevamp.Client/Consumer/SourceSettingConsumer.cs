using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskSourceDto;

namespace TaskyRevamp.Client.Consumer
{
    public class SourceSettingConsumer
    {
        private readonly TaskyService _taskyService;

        public SourceSettingConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }
        public async Task<CommonApiResponse<List<SourceDto>>> GetTaskSourcesForDDL(bool isload = true)
        {


            var url = $"api/SourceSetting/GetTaskSourcesForDDL";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<SourceDto>>>(url,isload);

            return res;
        }
        public async Task<CommonApiResponse<List<SourceDto>>> GetTaskSourceFillter(bool isload = true)
        {


            var url = $"api/SourceSetting/GetTaskSourcesForFilter";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<SourceDto>>>(url, isload);

            return res;
        }
        public async Task<CommonApiResponse<PagedResult<SourceDtoWithName>>> GetSources(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchField> searchFields = null, string searchText = null)
        {
            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);

            var url = $"api/SourceSetting/GetSourceSettings{queryString}";

            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<SourceDtoWithName>>>(url);

            return res;
        }
        public async Task<CommonApiResponse<PagedResult<SourceDto>>> GetSourcesView(int pageNumber, int pageSize, bool IsCompleted = false,bool Isload=true)
        {
            var query = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"pageSize={pageSize}",
                $"IsCompleted={IsCompleted}"
            };
            var queryString = "?" + string.Join("&", query);
            var url = $"api/SourceSetting/GetSourceSettingsView{queryString}";

            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<SourceDto>>>(url,Isload);

            return res;
        }
        public async Task<CommonApiResponse<List<SourceDto>>> GetSourcesWithoutPagination()
        {
            var url = $"api/SourceSetting/GetSourceWithoutPagination";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<SourceDto>>>(url);
            return res;
        }
        public async Task<CommonApiResponse<SourceDto>> GetSourceById(Guid id)
        {
            var url = $"api/SourceSetting/GetSourceSettingById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<SourceDto>>(url);
            return res;
        }

        public async Task<CommonApiResponse<bool>> CreateSource(SourceDto SourceDto)
        {
            var url = $"api/SourceSetting/CreateSource";
            var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

            return res;
        }


        public async Task<CommonApiResponse<bool>> UpdateSource(SourceDto SourceDto)
        {
            var url = $"api/SourceSetting/UpdateSource";
            var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

            return res;
        }
        public async Task<CommonApiResponse<bool>> RetriveSource(SourceDto SourceDto)
        {
            var url = $"api/SourceSetting/RetriveSource";
            var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

            return res;
        }

        public async Task<CommonApiResponse<bool>> RetriveDeleteSource(SourceDto SourceDto)
        {
            var url = $"api/SourceSetting/RetriveDeleteSource";
            var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

            return res;
        }
        public async Task<CommonApiResponse<bool>> CheckRelatedComplatedTaskitemSource(SourceDto SourceDto)
        {
            var url = $"api/SourceSetting/CheckRelatedComplatedTaskitemSource";
            var res = await _taskyService.PostJsonAsync<bool>(url, SourceDto);

            return res;
        }
        public async Task<CommonApiResponse<bool>> DeleteSource(Guid id)
        {
            var url = $"api/SourceSetting/DeleteSource/{id}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
    }
}
