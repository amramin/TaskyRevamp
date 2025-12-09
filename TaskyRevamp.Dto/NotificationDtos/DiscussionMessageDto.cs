using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dtos.Discussion
{
    public class DiscussionMessageDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }

        public DateTime MessageDate { get; set; }
        public Guid? FileId { get; set; }
        public string? UserName { set; get; }
        public Guid UserId { get; set; }
        public Guid MemberId { get; set; }
        public Guid DiscussionId { get; set; }

        //  public FileDto? File { get; set; }


    }
}