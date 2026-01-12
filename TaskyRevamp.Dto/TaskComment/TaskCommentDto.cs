using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.TaskComment
{
    public class TaskCommentDto
    {
        public Guid Id { get; set; }
        public Guid TaskItemId { get; set; }
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public string Content { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
    public class TaskCommentWithNameDto
    {
        public TaskCommentDto TaskComment { get; set; }
        public string? CreatedByName { get; set; }
        public string? UpdatedByName { get; set; }
    }
}
