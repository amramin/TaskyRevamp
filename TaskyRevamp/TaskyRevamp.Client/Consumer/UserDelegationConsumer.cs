using System.Net.Http.Json;
using TaskyRevamp.Client;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.UserDelegation;

namespace TaskyRevamp.Client.Consumer
{
    public class UserDelegationConsumer
    {
        private readonly TaskyService _taskyService;

        public UserDelegationConsumer(TaskyService taskyService)
        {
            _taskyService = taskyService;
        }

     
        public async Task<CommonApiResponse<PagedResult<UserDelegationDto>>> GetAllUserDelegation(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDelegation> searchFields = null, string searchText = null)
        {

            var queryString = _taskyService.PreparePaginatedSearchQueryString(pageNumber, pageSize, sortByColumnName, sortAscending, searchFields, searchText);

            var url = $"api/UserDelegation/GetAllUserDelegation{queryString}";


            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<PagedResult<UserDelegationDto>>>(url);

            return res;
        }
        public async Task<bool> ChangeActiveValue(Guid id)
        {
            var response = await _taskyService.httpClient.PutAsJsonAsync($"api/UserDelegation/ChangeActiveValue", new GuidDto() { Id = id });
            return response.IsSuccessStatusCode;
        }
        public async Task CreateUserDelegation(UserDelegationDto UserDelegation)
        {
            await _taskyService.PostAsJsonAsync($"api/UserDelegation/", UserDelegation);
        }
        public async Task<CommonApiResponse<UserDelegationDto>> GetById(string id)
        {
            var res = await _taskyService.GetFromJsonAsync<CommonApiResponse<UserDelegationDto>>($"api/UserDelegation/{id}");


            return res;
        }


        public async Task<CommonApiResponse<bool>> Delete(string id)
        {
            var res = await _taskyService.DeleteFromJsonAsync<CommonApiResponse<bool>>($"api/UserDelegation/{id}");
            return res;
        }
        public async Task update(UserDelegationDto UserDelegation)
        {
            await _taskyService.PutAsJsonAsync($"api/UserDelegation", UserDelegation);
        }




    }
}
