using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using ViewTaskSetting = TaskyRevamp.Domain.Models.SystemConfiguration.ViewTaskSettings;

namespace TaskyRevamp.Services.SystemConfiguration.ViewTaskSettings.Query
{
	public record GetActiveViewTaskSettingsQuery() : IRequest<List<ViewTaskSettingsDto>>;
	public class GetActiveViewTaskSettingsHandler : IRequestHandler<GetActiveViewTaskSettingsQuery, List<ViewTaskSettingsDto>>
	{
		private readonly IRepository<ViewTaskSetting> _ViewTaskRepository;

		public GetActiveViewTaskSettingsHandler(IRepository<ViewTaskSetting> _viewTaskRepository)
		{
			_ViewTaskRepository = _viewTaskRepository;
		}
		public async Task<List<ViewTaskSettingsDto>> Handle(GetActiveViewTaskSettingsQuery request, CancellationToken cancellationToken)
		{
			List<ViewTaskSettingsDto> ViewTasks = new List<ViewTaskSettingsDto>();
			var ViewTasksResponce = await _ViewTaskRepository.AllAsNoTracking();
			if (ViewTasksResponce.Success && ViewTasksResponce.Value != null && ViewTasksResponce.Value.Any())
			{
				ViewTasks = ViewTasksResponce.Value.Where(v => v.IsActive == true).Select(v => v.CopyToDto()).ToList();
			}
			return ViewTasks;
		}
	}
}
