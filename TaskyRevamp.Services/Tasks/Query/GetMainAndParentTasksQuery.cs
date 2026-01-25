using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query
{
	public record GetMainAndParentTasksQuery() : IRequest<List<CreateTaskDto>>;
	public class GetMainAndParentTasksHandler : IRequestHandler<GetMainAndParentTasksQuery, List<CreateTaskDto>>
	{
		private readonly IRepository<TaskItem> _taskRepository;
		public GetMainAndParentTasksHandler(IRepository<TaskItem> taskRepository) => _taskRepository = taskRepository;
		public async Task<List<CreateTaskDto>> Handle(GetMainAndParentTasksQuery request, CancellationToken cancellationToken)
		{
			List<CreateTaskDto> taskDtos = new List<CreateTaskDto>();
			var res = await _taskRepository.FindBy(t => ((t.Parent == null && !t.Subtasks.Any()) || t.Subtasks.Any()) && 
				t.TaskTypeId != Guid.Empty && t.TaskSourceId != Guid.Empty && t.PriorityId != Guid.Empty);
			if (res.Success && res.Value != null && res.Value.Any())
			{
				var tasks = res.Value;
				taskDtos = tasks.Select(t => t.CopyToDto()).ToList();	
			}
			return taskDtos;
		}
	}
}
