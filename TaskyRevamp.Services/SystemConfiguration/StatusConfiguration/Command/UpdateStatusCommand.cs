using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using statusSetting = TaskyRevamp.Domain.Models.SystemConfiguration.StatusSettings;
namespace TaskyRevamp.Services.SystemConfiguration.StatusConfiguration.Command
{
	public record UpdateStatusCommand(StatusSettingsDto StatusSettingsDto) : IRequest<bool>;
	public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, bool>
	{
		private readonly IRepository<statusSetting> _StatusRepository;
		public UpdateStatusHandler(IRepository<statusSetting> _statusRepository)
		{
			_StatusRepository = _statusRepository;
		}

		public async Task<bool> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
		{
			var resStatus = await _StatusRepository.FindByKey(request.StatusSettingsDto.Id);
			if (resStatus.Success && resStatus != null && resStatus.Value != null)
			{
				var status = resStatus.Value;
				status.NameEnglish = request.StatusSettingsDto.NameEnglish;
				status.NameArabic = request.StatusSettingsDto.NameArabic;
				status.NameColor = request.StatusSettingsDto.NameColor;
				status.BackgroundColor = request.StatusSettingsDto.BackgroundColor;

				await _StatusRepository.Update(status);
				await _StatusRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
