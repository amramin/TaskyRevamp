using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddTaskSetting = TaskyRevamp.Domain.Models.SystemConfiguration.AddTaskSettings;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.SystemConfiguration.AddTaskSettings.Command
{

    public record UpdateAddTaskSettingsCommand(List<AddTaskSettingDto> AddTaskSettings) : IRequest<bool>;

    public class UpdateAddTaskSettings : IRequestHandler<UpdateAddTaskSettingsCommand, bool>
    {
        private readonly IRepository<AddTaskSetting> _addTaskSettingRepository;

        public UpdateAddTaskSettings(IRepository<AddTaskSetting> addTaskSettingRepository)
        {
            _addTaskSettingRepository = addTaskSettingRepository;
        }

        public async Task<bool> Handle(UpdateAddTaskSettingsCommand request, CancellationToken cancellationToken)
        {

            foreach (var setting in request.AddTaskSettings)
            {
                var existingSettingResponse = await _addTaskSettingRepository.FindByKey(setting.Id);
                if (existingSettingResponse != null && existingSettingResponse.Success && existingSettingResponse.Value != null)
                {
                    var existingSetting = existingSettingResponse.Value;
                    existingSetting.Update(setting);
                    await _addTaskSettingRepository.Update(existingSetting);
                }
            }

            return true;
        }

    }
}
