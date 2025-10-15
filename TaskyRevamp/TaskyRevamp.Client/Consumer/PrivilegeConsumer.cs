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
    }
}
