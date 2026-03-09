using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands
{
	public record DeleteTasksCommand(List<Guid> TaskIds) : IRequest<bool>;
	public class DeleteTasksHandler : IRequestHandler<DeleteTasksCommand, bool>
	{
		private readonly IRepository<TaskItem> _taskRepository;
		public DeleteTasksHandler(IRepository<TaskItem> taskRepository)
		{
			_taskRepository = taskRepository;
		}
		public async Task<bool> Handle(DeleteTasksCommand request, CancellationToken cancellationToken)
		{
			await _taskRepository.DeleteRang(request.TaskIds);
			return true;
		}
	}
}
