using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskDto;
using TaskItems = TaskyRevamp.Domain.Models.Task.TaskItem;

namespace TaskyRevamp.Services.Users.Command
{
	public record DeleteUserCommand(Guid userId) : IRequest<bool>;
	public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
	{
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<TaskItems> _taskRepository;
		public DeleteUserHandler(IRepository<User> userRepository, IRepository<TaskItems> taskRepository)
		{
			_userRepository = userRepository;
			_taskRepository = taskRepository;
		}

		public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			List<CreateTaskDto> tasks = new List<CreateTaskDto>();
			var res = await _taskRepository.AllAsNoTracking(includeProperties: $"{nameof(TaskItems.status)}");
			if (res.Success && res.Value != null)
			{
				tasks = res.Value.Select(t => t.CopyToDto()).ToList();
			}
			foreach (var task in tasks)
			{
				var hasUser = task.AssignedIds?.Any(id => id == request.userId) ?? false;
				var hasOpenStatus = task.TaskStatusName == "Completed";
				if (hasUser && hasOpenStatus)
				{
					var resUser = await _userRepository.FindBy(u => u.Id == request.userId);
					if (resUser.Success && resUser.Value != null)
					{
						var user = resUser.Value.First();
						user.IsDeleted = true;
						await _userRepository.Update(user);
						await _userRepository.SaveChangesAsync();
					}
				}
			}
			
			return true;
		}
	}
}
