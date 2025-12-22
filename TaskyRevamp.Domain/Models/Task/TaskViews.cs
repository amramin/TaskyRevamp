using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.TaskViews;

namespace TaskyRevamp.Domain.Models.Task
{
	public class TaskViews : Entity
	{
		public Guid TaskItemId { get; private set; }
		public TaskItem TaskItem { get; set; }
		public Guid UserId { get; private set; }
		public User User { get; set; }
		public DateTime ViewedAt { get; set; }

		public TaskViews() { }
		public TaskViews(Guid taskItemId, Guid userId)
		{
			Id = Guid.NewGuid();
			TaskItemId = taskItemId;
			UserId = userId;
			ViewedAt = DateTime.UtcNow;
		}
		public void UpdateViewedTime()
		{
			ViewedAt = DateTime.UtcNow;
		}
		public TaskViewsDto CopyToDto()
		{
			return new TaskViewsDto
			{
				Id = Id,
				TaskItemId = TaskItemId,
				UserId = UserId,
				ViewdAt = ViewedAt,
			};
		}
	}
}
