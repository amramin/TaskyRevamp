using System;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class PinnedTasks
{
    public Guid TaskId { get; set; }
    public TaskItem Task { get; set; }
    public DateTime PinnedAt { get; set; }
    public User PinnedBy { get; set; }
}
