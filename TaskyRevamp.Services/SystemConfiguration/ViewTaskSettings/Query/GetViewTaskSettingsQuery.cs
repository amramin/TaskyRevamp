using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using ViewTasSetting = TaskyRevamp.Domain.Models.SystemConfiguration.ViewTaskSettings;

namespace TaskyRevamp.Services.SystemConfiguration.ViewTaskSettings.Query
{
	public record GetViewTaskSettingsQuery(): IRequest<List<ViewTaskSettingsDto>>;
	public class GetViewTaskSettingsQueryHandler : IRequestHandler<GetViewTaskSettingsQuery, List<ViewTaskSettingsDto>>
	{
		private readonly IRepository<ViewTasSetting> _ViewTaskRepository;

		public GetViewTaskSettingsQueryHandler(IRepository<ViewTasSetting> _viewTaskRepository)
		{
			_ViewTaskRepository = _viewTaskRepository; 	
		}
		public async Task<List<ViewTaskSettingsDto>> Handle(GetViewTaskSettingsQuery request, CancellationToken cancellationToken)
		{
			List<ViewTaskSettingsDto> ViewTasks = new List<ViewTaskSettingsDto>();
			var ViewTasksResponce = await _ViewTaskRepository.AllAsNoTracking();
			if(ViewTasksResponce.Success && ViewTasksResponce.Value != null && ViewTasksResponce.Value.Any())
			{
				ViewTasks = ViewTasksResponce.Value.Select(v => v.CopyToDto()).ToList();
			}
			return ViewTasks;
		}
	}
}
