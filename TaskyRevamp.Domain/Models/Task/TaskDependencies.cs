using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TaskyRevamp.Domain.Models.Users;

namespace TaskyRevamp.Domain.Models.Task;

public class TaskDependencies : Entity
{
    public Guid TaskItemId { get; set; }          
    public Guid DependentId { get; set; }          

    public TaskItem Task { get; set; }            
}