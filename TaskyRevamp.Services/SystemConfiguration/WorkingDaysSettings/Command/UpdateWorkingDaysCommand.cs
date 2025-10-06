using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using WorkingDaySetting = TaskyRevamp.Domain.Models.SystemConfiguration.WorkingDaysSettings;

namespace TaskyRevamp.Services.SystemConfiguration.WorkingDaysSettings.Command
{
	public record UpdateWorkingDaysCommand(List<WorkingDaysSettingsDto> WorkingDaysSettingsDtos) :  IRequest<bool>;
	public class UpdateWorkingDaysCommandHandler : IRequestHandler<UpdateWorkingDaysCommand, bool>
	{
		private readonly IRepository<WorkingDaySetting> _WorkingDaySettingRepository;
		public UpdateWorkingDaysCommandHandler(IRepository<WorkingDaySetting> _workingDaySettingRepository)
		{
			_WorkingDaySettingRepository = _workingDaySettingRepository;	
		}
		public async Task<bool> Handle(UpdateWorkingDaysCommand request, CancellationToken cancellationToken)
		{
			var newSettings = request.WorkingDaysSettingsDtos.ToList();
			foreach (var DaySetting in newSettings)
			{
				var originalSetting = await _WorkingDaySettingRepository.FindByKey(DaySetting.Id);
				if (originalSetting != null && originalSetting.Value != null)
				{
					if(originalSetting.Value.IsActive != DaySetting.IsActive)
					{
						originalSetting.Value.IsActive = DaySetting.IsActive;
						await _WorkingDaySettingRepository.Update(originalSetting.Value);
					}
				}
			}
			return true;
		}
	}
}
