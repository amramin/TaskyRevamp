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
	public record UpdatePrioritiesOrder(List<PriorityDto> PriorityDtos):IRequest<bool>;
	public class UpdatePrioritiesOrderCommandHanler : IRequestHandler<UpdatePrioritiesOrder, bool>
	{
		private readonly IRepository<PrioritySetting> _priorityRepository;

		public UpdatePrioritiesOrderCommandHanler(IRepository<PrioritySetting> priorityRepository)
		{
			this._priorityRepository = priorityRepository;
		}

		public async Task<bool> Handle(UpdatePrioritiesOrder request, CancellationToken cancellationToken)
		{
			var editedPriorities = request.PriorityDtos.ToList();
			foreach (var priority in editedPriorities)
			{
				var editedpriority = await _priorityRepository.FindByKey(priority.Id);
				if (editedpriority != null && editedpriority.Success && editedpriority.Value != null)
				{
					if (editedpriority.Value.Order != priority.Order)
					{
						editedpriority.Value.Order = priority.Order;
						await _priorityRepository.Update(editedpriority.Value);
					}
				}
			}
			await _priorityRepository.SaveChangesAsync();
			return true;
		}
	}
}
