using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using ViewTaskSetting = TaskyRevamp.Domain.Models.SystemConfiguration.ViewTaskSettings;

namespace TaskyRevamp.Services.SystemConfiguration.ViewTaskSettings.Command
{
	public record UpdateViewTaskSettingActivationCommand(List<ViewTaskSettingsDto> TaskViews):IRequest<bool>;
	public class UpdateViewTaskSettingActivationHandler : IRequestHandler<UpdateViewTaskSettingActivationCommand, bool>
	{
		private readonly IRepository<ViewTaskSetting> _viewTaskRepository;
        private readonly IRepository<DefaultViewSettings> _defaultviewTaskRepository;
        public UpdateViewTaskSettingActivationHandler(IRepository<ViewTaskSetting> viewTaskRepository, IRepository<DefaultViewSettings> defaultviewTaskRepository)
		{
			_viewTaskRepository = viewTaskRepository;
			_defaultviewTaskRepository = defaultviewTaskRepository;
		}
		public async Task<bool> Handle(UpdateViewTaskSettingActivationCommand request, CancellationToken cancellationToken)
		{
			var ViewTaskResponse =  request.TaskViews.ToList();
			var DefaultSelected =  _defaultviewTaskRepository.AllAsNoTracking().Result.Value.FirstOrDefault();
			if(DefaultSelected is not null)
			{
				if(ViewTaskResponse.Any(p => !p.IsActive && (int)p.ViewType == DefaultSelected.DefaultSelected))
				{
					throw new Exception("cannotdeactivate");
				}
			}
            foreach (var viewTask in ViewTaskResponse)
			{
				var originTaskViewRow = await _viewTaskRepository.FindByKey(viewTask.Id);
				if (originTaskViewRow.Success && originTaskViewRow != null && originTaskViewRow.Value != null) {
					if (originTaskViewRow.Value.IsActive != viewTask.IsActive) {
						originTaskViewRow.Value.IsActive = viewTask.IsActive;
						await _viewTaskRepository.Update(originTaskViewRow.Value);
					}
				}	
			}
			await _viewTaskRepository.SaveChangesAsync();
			return true;
		}
	}
}
