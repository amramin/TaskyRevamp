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
using TaskyRevamp.Services.Exceptions;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
	public record UpdateSourceCommand(SourceDto SourceDto) : IRequest<bool>;
	public class UpdateSourceHandler : IRequestHandler<UpdateSourceCommand, bool>
	{
		private readonly IRepository<Sources> _sourceRepository;
		private readonly IStringLocalizer<SharedResources> _localizer;
		public UpdateSourceHandler(IRepository<Sources> sourceRepository, IStringLocalizer<SharedResources> localizer)
		{
			_sourceRepository = sourceRepository;
			_localizer = localizer;
		}
		public async Task<bool> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
		{
			await ValidateSource(request.SourceDto);
			var res = await _sourceRepository.FindByKey(request.SourceDto.Id);
			if (res.Success && res != null && res.Value != null)
			{
				var sourceData = res.Value;
				var newData = request.SourceDto;

				sourceData.NameEnglish = newData.NameEnglish;
				sourceData.NameArabic = newData.NameArabic;
				sourceData.IsActive = newData.IsActive;

				await _sourceRepository.Update(sourceData);
				await _sourceRepository.SaveChangesAsync();
			}
			return true;
		}
		private async Task ValidateSource(SourceDto _sourceDto)
		{
			var exists = await _sourceRepository.FindBy(s => s.Id != _sourceDto.Id && (s.NameEnglish.ToLower() == _sourceDto.NameEnglish.ToLower() || s.NameArabic.ToLower() == _sourceDto.NameArabic.ToLower()));
			if (exists?.Value?.Count > 0)
			{
				var sources = exists.Value;
				var errors = new Dictionary<string, List<string>>();
				if (sources.Any(x => string.Equals(x.NameEnglish, _sourceDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
					errors.Add(nameof(SourceDto.NameEnglish), new List<string> { _localizer["SourceRequiredValidation"] });
				if (sources.Any(x => string.Equals(x.NameArabic, _sourceDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
					errors.Add(nameof(SourceDto.NameArabic), new List<string> { _localizer["SourceRequiredValidation"] });

				throw new ValidationException(errors);
			}
		}
	}
}
