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
	public record GetViewTaskSettingsQuery(): IRequest<List<ViewTaskSettingsDto>>;
	public class GetViewTaskSettingsHandler : IRequestHandler<GetViewTaskSettingsQuery, List<ViewTaskSettingsDto>>
	{
		private readonly IRepository<ViewTaskSetting> _viewTaskRepository;

		public GetViewTaskSettingsHandler(IRepository<ViewTaskSetting> viewTaskRepository)
		{
			_viewTaskRepository = viewTaskRepository; 	
		}
		public async Task<List<ViewTaskSettingsDto>> Handle(GetViewTaskSettingsQuery request, CancellationToken cancellationToken)
		{
			List<ViewTaskSettingsDto> ViewTasks = new List<ViewTaskSettingsDto>();
			var ViewTasksResponce = await _viewTaskRepository.AllAsNoTracking();
			if(ViewTasksResponce.Success && ViewTasksResponce.Value != null && ViewTasksResponce.Value.Any())
			{
				ViewTasks = ViewTasksResponce.Value.Select(v => v.CopyToDto()).ToList();
			}
			return ViewTasks;
		}
	}
}
