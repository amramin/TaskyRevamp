using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Dto.TaskDto;

namespace TaskyRevamp.Services.Tasks.Query
{
	public record GetTaskByIdQuery(Guid Id) : IRequest<CreateTaskDto>;
	public class GetOneTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, CreateTaskDto>
	{
		private readonly ITaskRepository _taskRepository;

		public GetOneTaskByIdHandler(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}
		public async Task<CreateTaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
		{
			var res = await _taskRepository.GetTaskById(request.Id);
			if (res is null)
			{
				throw new Exception("Task not found");
			}
			var taskDto = res.ToDto();
			return taskDto;
		}
	}
}
