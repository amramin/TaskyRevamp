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
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
	public record CreateTypeCommand(TypeDto TypeDto) : IRequest<bool>;
	public class CreateTypeHandler : IRequestHandler<CreateTypeCommand, bool>
	{
		private readonly IRepository<Types> _typeRepository;
		private readonly IStringLocalizer<SharedResources> _localizer;
		public CreateTypeHandler(IRepository<Types> typeRepository, IStringLocalizer<SharedResources> localizer)
		{
			_typeRepository = typeRepository;
			_localizer = localizer;
		}
		public async Task<bool> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
		{
			await ValidateType(request.TypeDto);
			var type = new Types
			{
				NameEnglish = request.TypeDto.NameEnglish,
				NameArabic = request.TypeDto.NameArabic,
				IsActive = request.TypeDto.IsActive,
			};
			await _typeRepository.Insert(type);
			await _typeRepository.SaveChangesAsync();
			return true;
		}
		private async Task ValidateType(TypeDto _typeDto)
		{
			var exists = await _typeRepository.FindBy(t => t.Id != _typeDto.Id && (t.NameEnglish.ToLower() == _typeDto.NameEnglish.ToLower() || t.NameArabic.ToLower() == _typeDto.NameArabic.ToLower()));
			if (exists?.Value?.Count > 0)
			{
				var types = exists.Value;
				var errors = new Dictionary<string, List<string>>();
				if (types.Any(x => string.Equals(x.NameEnglish, _typeDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
					errors.Add(nameof(TypeDto.NameEnglish), new List<string> { _localizer["TypeRequiredValidation"] });
				if (types.Any(x => string.Equals(x.NameArabic, _typeDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
					errors.Add(nameof(TypeDto.NameArabic), new List<string> { _localizer["TypeRequiredValidation"] });

				throw new ValidationException(errors);
			}
		}
	}
}
