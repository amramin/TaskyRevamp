using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddTaskSetting = TaskyRevamp.Domain.Models.SystemConfiguration.AddTaskSettings;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SystemConfiguration.AddTaskSettings.Query
{
    public record GetAddTaskSettingsQuery() : IRequest<List<AddTaskSettingDto>>;

    public class GetGetAddTaskSettingsHandler : IRequestHandler<GetAddTaskSettingsQuery, List<AddTaskSettingDto>>
    {
        private readonly IRepository<AddTaskSetting> _addTaskSettingRepository;

        public GetGetAddTaskSettingsHandler(IRepository<AddTaskSetting> addTaskSettingRepository)
        {
            _addTaskSettingRepository = addTaskSettingRepository;
        }

        public async Task<List<AddTaskSettingDto>> Handle(GetAddTaskSettingsQuery request, CancellationToken cancellationToken)
        {
            List<AddTaskSettingDto> addTaskSettingsDto = new List<AddTaskSettingDto>();

            var settingsResponse = await _addTaskSettingRepository.AllAsNoTracking();

            if (settingsResponse.Success && settingsResponse.Value != null && settingsResponse.Value.Any())
            {
                addTaskSettingsDto = settingsResponse.Value.Select(x => x.CopyToDto()).OrderBy(x => x.Order).ToList();
            }

            return addTaskSettingsDto;
        }
    }
}
