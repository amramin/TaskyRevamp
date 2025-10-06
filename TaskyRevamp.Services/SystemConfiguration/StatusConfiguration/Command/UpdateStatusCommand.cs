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
		private readonly IRepository<statusSetting> _statusRepository;
		public UpdateStatusHandler(IRepository<statusSetting> statusRepository)
		{
			this._statusRepository = statusRepository;
		}

		public async Task<bool> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
		{
			var resStatus = await _statusRepository.FindByKey(request.StatusSettingsDto.Id);
			if (resStatus.Success && resStatus != null && resStatus.Value != null)
			{
				var status = resStatus.Value;
				status.NameEnglish = request.StatusSettingsDto.NameEnglish;
				status.NameArabic = request.StatusSettingsDto.NameArabic;
				status.NameColor = request.StatusSettingsDto.NameColor;
				status.BackgroundColor = request.StatusSettingsDto.BackgroundColor;

				await _statusRepository.Update(status);
				await _statusRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
