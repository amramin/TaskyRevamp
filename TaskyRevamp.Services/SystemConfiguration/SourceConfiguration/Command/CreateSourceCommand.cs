using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using Source = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
	public record CreateSourceCommand(SourceDto SourceDto) : IRequest<bool>;
	public class CreateSourceHandler : IRequestHandler<CreateSourceCommand, bool>
	{
		private readonly IRepository<Source> _sourceRepository;
		private readonly IStringLocalizer<SharedResources> _localizer;
		public CreateSourceHandler(IRepository<Source> sourceRepository, IStringLocalizer<SharedResources> localizer)
		{
			_sourceRepository = sourceRepository; 
			_localizer = localizer;
		}
		public async Task<bool> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
		{
			await ValidateSource(request.SourceDto);
			var source = new Source
			{
				NameEnglish = request.SourceDto.NameEnglish,
				NameArabic = request.SourceDto.NameArabic,
				IsActive = request.SourceDto.IsActive,
			};
			await _sourceRepository.Insert(source);
			await _sourceRepository.SaveChangesAsync();
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
