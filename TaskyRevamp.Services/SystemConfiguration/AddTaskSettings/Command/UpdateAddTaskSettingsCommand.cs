using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
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
        private readonly IRepository<TaskDependencies> _taskDependencies;
        private readonly IStringLocalizer<SharedResources> _localizer;

		public UpdateAddTaskSettings(IRepository<AddTaskSetting> addTaskSettingRepository, IStringLocalizer<SharedResources> localizer, IRepository<TaskDependencies> taskDependencies)
		{
			_addTaskSettingRepository = addTaskSettingRepository;
			_localizer = localizer;
			_taskDependencies = taskDependencies;
		}

		public async Task<bool> Handle(UpdateAddTaskSettingsCommand request, CancellationToken cancellationToken)
        {
            bool wasInactive = false;
            var existingDependencyRes = await _addTaskSettingRepository.FindBy(s => s.NameEnglish == "Dependency");
			if (existingDependencyRes.Success && existingDependencyRes.Value != null)
			{
				var existingDependency = existingDependencyRes.Value.FirstOrDefault();
                wasInactive = existingDependency != null && !existingDependency.IsActive;
			}
			var newDependency = request.AddTaskSettings.FirstOrDefault(s => s.NameEnglish == "Dependency");
            var isNowActive = newDependency != null && newDependency.IsActive;

			if (wasInactive && isNowActive)
				await _taskDependencies.DeleteAll();

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
