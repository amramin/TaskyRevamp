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
		private readonly IRepository<PrioritySetting> _PriorityRepository;

		public UpdatePrioritiesOrderCommandHanler(IRepository<PrioritySetting> _priorityRepository)
		{
			_PriorityRepository = _priorityRepository;
		}

		public async Task<bool> Handle(UpdatePrioritiesOrder request, CancellationToken cancellationToken)
		{
			var EditedPriorities = request.PriorityDtos.ToList();
			foreach (var priority in EditedPriorities)
			{
				var editedpriority = await _PriorityRepository.FindByKey(priority.Id);
				if (editedpriority != null && editedpriority.Success && editedpriority.Value != null)
				{
					if (editedpriority.Value.Order != priority.Order)
					{
						editedpriority.Value.Order = priority.Order;
						await _PriorityRepository.Update(editedpriority.Value);
					}
				}
			}
			await _PriorityRepository.SaveChangesAsync();
			return true;
		}
	}
}
