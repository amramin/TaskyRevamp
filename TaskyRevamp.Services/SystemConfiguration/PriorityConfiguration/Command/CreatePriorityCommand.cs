using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Localization.Resources;
using TaskyRevamp.Services.Exceptions;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
	public record CreatePriorityCommand(PriorityDto PriorityDto):IRequest<bool>;
	public class CreatePriorityHandler : IRequestHandler<CreatePriorityCommand, bool>
	{
		private readonly IRepository<PrioritySetting> _priorityRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CreatePriorityHandler(IRepository<PrioritySetting> priorityRepositry, IStringLocalizer<SharedResources> localizer)
        {
            _priorityRepository = priorityRepositry;
            _localizer = localizer;
        }
        public async Task<bool> Handle(CreatePriorityCommand request, CancellationToken cancellationToken)
		{
            await ValidatePriority(request.PriorityDto);

            var priorityDto = request.PriorityDto;

            var priority = new PrioritySetting(
				priorityDto.NameEnglish,
				priorityDto.NameArabic,
				priorityDto.NameColor,
				priorityDto.BackgroundColor,
				priorityDto.Order
			); 

			await _priorityRepository.Insert(priority);
			await _priorityRepository.SaveChangesAsync();

			return true;
		}

        private async Task ValidatePriority(PriorityDto priorityDto)
        {
            var exists = await _priorityRepository.FindBy(p => p.Id != priorityDto.Id 
                && (p.NameEnglish.ToLower() == priorityDto.NameEnglish.ToLower() 
                || p.NameArabic.ToLower() == priorityDto.NameArabic.ToLower()));
            if (exists?.Value?.Count > 0)
            {
                var priorities = exists.Value;
                var errors = new Dictionary<string, List<string>>();
                if (priorities.Any(x => string.Equals(x.NameEnglish, priorityDto.NameEnglish, StringComparison.OrdinalIgnoreCase)))
                    errors.Add(nameof(PriorityDto.NameEnglish), new List<string> { _localizer["PriorityDuplicateValidation"] });
                if (priorities.Any(x => string.Equals(x.NameArabic, priorityDto.NameArabic, StringComparison.OrdinalIgnoreCase)))
                    errors.Add(nameof(PriorityDto.NameArabic), new List<string> { _localizer["PriorityDuplicateValidation"] });

                throw new ValidationException(errors);
            }
        }
    }
}
