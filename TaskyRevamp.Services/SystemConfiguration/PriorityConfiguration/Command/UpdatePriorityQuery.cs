using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
	public record UpdateProrityQuery(PriorityDto PriorityDto): IRequest<bool>;
	public class UpdatePriorityQueryHandler : IRequestHandler<UpdateProrityQuery, bool>
	{
		private readonly IRepository<PrioritySetting> _PriorityRepository;

		public UpdatePriorityQueryHandler(IRepository<PrioritySetting> _priorityRepository)
		{
			_PriorityRepository = _priorityRepository;
		}
		public async Task<bool> Handle(UpdateProrityQuery request, CancellationToken cancellationToken)
		{
			var resPriority = await _PriorityRepository.FindByKey(request.PriorityDto.Id);
			if (resPriority.Success && resPriority != null && resPriority.Value != null)
			{
				var priority = resPriority.Value;
				priority.NameEnglish = request.PriorityDto.NameEnglish;
				priority.NameArabic = request.PriorityDto.NameArabic;
				priority.NameColor = request.PriorityDto.NameColor;
				priority.BackgroundColor = request.PriorityDto.BackgroundColor;
				priority.Order = request.PriorityDto.Order;

				await _PriorityRepository.Update(priority);
				await _PriorityRepository.SaveChangesAsync();
			}
			return true;
		}	
	}
}

