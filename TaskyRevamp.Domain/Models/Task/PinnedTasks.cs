using System;
using System.Reflection.Metadata;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.PinnedTasks;
using TaskyRevamp.Dto.TaskComment;

namespace TaskyRevamp.Domain.Models.Task;

public class PinnedTasks : Entity, IHasCreationMetaData
{
    public Guid TaskId { get; set; }
    public TaskItem Task { get; set; }
    //public DateTime PinnedAt { get; set; }
   // public User PinnedBy { get; set; }
    public Guid CreatedById { get ; set ; }
    public DateTime CreateDate { get ; set ; }
    public User CreatedBy { get ; set ; }
    public bool SetData(PinnedTasksDto pinnedTasksDto)
    {
        Id = Id;
        TaskId = pinnedTasksDto.TaskId;
        CreatedById = pinnedTasksDto.CreatedById;
        CreateDate = pinnedTasksDto.CreateDate;
        return true;

    }
    public PinnedTasksDto CopyToDto()
    {
        return new PinnedTasksDto
        {
            Id = Id,
            TaskId = TaskId,
            CreateDate = CreateDate,
            CreatedById = CreatedById,




        };
    }

}
