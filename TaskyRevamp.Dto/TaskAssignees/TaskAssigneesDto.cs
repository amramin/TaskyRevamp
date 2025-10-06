using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskAssignees
{
    public class TaskAssigneesDto
    {
        public Guid Id { get; set; }
        public Guid taskId { get; set; }

        public Guid UserId { get; set; }
        public bool AllowComplete { get; set; }
        //private readonly List<User> _items = new();
        //public IReadOnlyCollection<User> Items => _items.AsReadOnly();
        //public IEnumerable<Department> Departments => _items.Select(u => u.Department).Distinct();
        public bool IsRejected { get; set; }
        public string? RejectReason { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
