using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
	public record UpdateSourceCommand(SourceDto SourceDto) : IRequest<bool>;
	public class UpdateSourceHandler : IRequestHandler<UpdateSourceCommand, bool>
	{
		private readonly IRepository<Sources> _sourceRepository;
		public UpdateSourceHandler(IRepository<Sources> sourceRepository)
		{
			_sourceRepository = sourceRepository;
		}
		public async Task<bool> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
		{
			var res = await _sourceRepository.FindByKey(request.SourceDto.Id);
			if (res.Success && res != null && res.Value != null)
			{
				var sourceData = res.Value;
				var newData = request.SourceDto;

				sourceData.NameEnglish = newData.NameEnglish;
				sourceData.NameArabic = newData.NameArabic;
				sourceData.IsActive = newData.IsActive;

				await _sourceRepository.Update(sourceData);
				//await _sourceRepository.SaveChangesAsync();
			}
			return true;
		}
	}
}
