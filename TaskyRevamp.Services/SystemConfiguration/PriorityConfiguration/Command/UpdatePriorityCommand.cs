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
//using TaskyRevamp.Domain.Exceptions;
using TaskyRevamp.Services.Exceptions;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
	public record UpdateProrityCommand(PriorityDto PriorityDto): IRequest<bool>;
	public class UpdateProrityHandler : IRequestHandler<UpdateProrityCommand, bool>
	{
		private readonly IRepository<PrioritySetting> _PriorityRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UpdateProrityHandler(IRepository<PrioritySetting> _priorityRepository, IStringLocalizer<SharedResources> localizer)
        {
            _PriorityRepository = _priorityRepository;
            _localizer = localizer;
        }
        public async Task<bool> Handle(UpdateProrityCommand request, CancellationToken cancellationToken)
		{
			await ValidatePriority(request.PriorityDto);

            var priorityDto = request.PriorityDto;
            var resPriority = await _PriorityRepository.FindByKey(priorityDto.Id);
			if (resPriority.Success && resPriority != null && resPriority.Value != null)
			{
				var priority = resPriority.Value;
				priority.NameEnglish = priorityDto.NameEnglish;
				priority.NameArabic = priorityDto.NameArabic;
				priority.NameColor = priorityDto.NameColor;
				priority.BackgroundColor = priorityDto.BackgroundColor;
				priority.Order = priorityDto.Order;

				await _PriorityRepository.Update(priority);
				await _PriorityRepository.SaveChangesAsync();
			}
			return true;
		}
		
		private async Task ValidatePriority(PriorityDto priorityDto)
		{
            var exists = await _PriorityRepository.FindBy(p => p.Id != priorityDto.Id && (p.NameEnglish.ToLower() == priorityDto.NameEnglish.ToLower() || p.NameArabic.ToLower() == priorityDto.NameArabic.ToLower()));
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

