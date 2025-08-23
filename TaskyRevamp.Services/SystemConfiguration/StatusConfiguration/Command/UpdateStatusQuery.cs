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
	public record UpdateStatusQuery(StatusSettingsDto StatusSettingsDto) : IRequest<bool>;
	public class UpdateStatusQueryHandler : IRequestHandler<UpdateStatusQuery, bool>
	{
		private readonly IRepository<statusSetting> _StatusRepository;
		public UpdateStatusQueryHandler(IRepository<statusSetting> _statusRepository)
		{
			_StatusRepository = _statusRepository;
		}

		public async Task<bool> Handle(UpdateStatusQuery request, CancellationToken cancellationToken)
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
