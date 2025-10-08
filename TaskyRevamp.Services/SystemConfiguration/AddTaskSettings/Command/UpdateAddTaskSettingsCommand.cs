using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using AddTaskSetting = TaskyRevamp.Domain.Models.SystemConfiguration.AddTaskSettings;

namespace TaskyRevamp.Services.SystemConfiguration.AddTaskSettings.Command
{

    public record UpdateAddTaskSettingsCommand(List<AddTaskSettingDto> AddTaskSettings) : IRequest<bool>;

    public class UpdateAddTaskSettings : IRequestHandler<UpdateAddTaskSettingsCommand, bool>
    {
        private readonly IRepository<AddTaskSetting> _addTaskSettingRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateAddTaskSettings(IRepository<AddTaskSetting> addTaskSettingRepository, IStringLocalizer<SharedResources> localizer)
        {
            _addTaskSettingRepository = addTaskSettingRepository;
            _localizer = localizer;
        }

        public async Task<bool> Handle(UpdateAddTaskSettingsCommand request, CancellationToken cancellationToken)
        {

            foreach (var setting in request.AddTaskSettings)
            {
                var existingSettingResponse = await _addTaskSettingRepository.FindByKey(setting.Id);
                if(existingSettingResponse == null || !existingSettingResponse.Success || existingSettingResponse.Value == null)
                {
                    throw new NotFoundException(_localizer[ApiError.AddTaskSettingNotFound].Value.Replace("{id}", setting.Id.ToString()));
                }

                var existingSetting = existingSettingResponse.Value;
                existingSetting.Update(setting);
                await _addTaskSettingRepository.Update(existingSetting);
                
            }

            return true;
        }

    }
}
