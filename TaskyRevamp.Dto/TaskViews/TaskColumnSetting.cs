using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskViews
{
    public class TaskColumnSetting
    {
        public string Id { get; set; }          // Unique key (e.g. "Title", "Status")
        public string DisplayName { get; set; } // Column name
        public int Order { get; set; }           // Grid order
        public bool IsVisible { get; set; }      // Show / Hide
                                                 // public bool IsMandatory { get; set; }    // e.g. Task Title
    }
}
