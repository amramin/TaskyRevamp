using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskTypeDto;

namespace TaskyRevamp.Client.Consumer
{
    public class TypeSettingConsumer
    {
        private readonly TaskyService _taskyService;

        public TypeSettingConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

        public async Task<CommonApiResponse<PagedResult<TypeDtoWithName>>> GetTypes(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchField> searchFields = null, string searchText = null)
        {
            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);

            var url = $"api/TypeSetting/GetTypeSettings{queryString}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<TypeDtoWithName>>>(url);

            return res;
        }
        public async Task<CommonApiResponse<List<TypeDto>>> GetTaskTypesForDDL()
        {


            var url = $"api/TypeSetting/GetTaskTypesForDDL";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<List<TypeDto>>>(url);

            return res;
        }

        public async Task<CommonApiResponse<TypeDto>> GetTypeById(Guid id)
        {
            var url = $"api/TypeSetting/GetTypeSettingById/{id}";
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<TypeDto>>(url);
            return res;
        }

        public async Task<CommonApiResponse<bool>> CreateType(TypeDto typeDto)
        {
            var url = $"api/TypeSetting/CreateType";
            var res = await _taskyService.PostJsonAsync<bool>(url, typeDto);

            return res;
        }

        public async Task<CommonApiResponse<bool>> UpdateType(TypeDto typeDto)
        {
            var url = $"api/TypeSetting/UpdateType";
            var res = await _taskyService.PostJsonAsync<bool>(url, typeDto);

            return res;
        }
        public async Task<CommonApiResponse<bool>> DeleteType(Guid id)
        {
            var url = $"api/TypeSetting/DeleteType/{id}";
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>(url);

            return res;
        }
    }
}
