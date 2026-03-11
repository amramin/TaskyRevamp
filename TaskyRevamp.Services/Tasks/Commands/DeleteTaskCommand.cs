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
	public record DeleteTaskCommand(Guid TaskId) : IRequest<bool>;
	public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
	{
		private readonly IRepository<TaskItem> _taskRepository;
		public DeleteTaskHandler(IRepository<TaskItem> taskRepository)
		{
			_taskRepository = taskRepository;
		}
		public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
		{
			await _taskRepository.Delete(request.TaskId);
			return true;
		}
	}
}
