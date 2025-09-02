using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.TaskChecklist
{
    public class TaskChecklistDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string TitleEnglish { get; set; }
        public string TitleArabic { get; set; }
    }
}
