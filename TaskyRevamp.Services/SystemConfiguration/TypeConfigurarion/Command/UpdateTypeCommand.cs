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
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Command
{
	public record UpdateTypeCommand(TypeDto TypeDto) : IRequest<bool>;
	public class UpdateTypeHandler : IRequestHandler<UpdateTypeCommand, bool>
	{
		private readonly IRepository<Types> _typeRepository;
		private readonly IStringLocalizer<SharedResources> _localizer;
		public UpdateTypeHandler(IRepository<Types> typeRepository, IStringLocalizer<SharedResources> localizer)
		{
			_typeRepository = typeRepository;
			_localizer = localizer;
		}
		public async Task<bool> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
		{
			await ValidateType(request.TypeDto);
			var res = await _typeRepository.FindByKey(request.TypeDto.Id);
			if (res.Success && res != null && res.Value != null)
			{
				var typeData = res.Value;
				var newData = request.TypeDto;

				typeData.NameEnglish = newData.NameEnglish;
				typeData.NameArabic = newData.NameArabic;
				typeData.IsActive = newData.IsActive;

				await _typeRepository.Update(typeData);
				await _typeRepository.SaveChangesAsync();
			}
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
