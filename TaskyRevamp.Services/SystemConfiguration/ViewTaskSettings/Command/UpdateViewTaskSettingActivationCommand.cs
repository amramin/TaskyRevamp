using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using ViewTaskSetting = TaskyRevamp.Domain.Models.SystemConfiguration.ViewTaskSettings;

namespace TaskyRevamp.Services.SystemConfiguration.ViewTaskSettings.Command
{
	public record UpdateViewTaskSettingActivationCommand(List<ViewTaskSettingsDto> TaskViews):IRequest<bool>;
	public class UpdateViewTaskSettingActivationCommandHandler : IRequestHandler<UpdateViewTaskSettingActivationCommand, bool>
	{
		private readonly IRepository<ViewTaskSetting> _ViewTaskRepository;

		public UpdateViewTaskSettingActivationCommandHandler(IRepository<ViewTaskSetting> _viewTaskRepository)
		{
			_ViewTaskRepository = _viewTaskRepository;
		}
		public async Task<bool> Handle(UpdateViewTaskSettingActivationCommand request, CancellationToken cancellationToken)
		{
			var ViewTaskResponse =  request.TaskViews.ToList();
			foreach (var viewTask in ViewTaskResponse)
			{
				var originTaskViewRow = await _ViewTaskRepository.FindByKey(viewTask.Id);
				if (originTaskViewRow.Success && originTaskViewRow != null && originTaskViewRow.Value != null) {
					if (originTaskViewRow.Value.IsActive != viewTask.IsActive) {
						originTaskViewRow.Value.IsActive = viewTask.IsActive;
						await _ViewTaskRepository.Update(originTaskViewRow.Value);
					}
				}	
			}
			await _ViewTaskRepository.SaveChangesAsync();
			return true;
		}
	}
}
