using TaskyRevamp.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Email;

public class SendEmailDto
{
    public string EmailToId { get; set; }
    public string EmailToName { get; set; }
    public string EmailSubject { get; set; }
    public string EmailBody { get; set; }
}

public class SendSurveyEmailDto : SendEmailDto
{
    public string SurveyLink { get; set; }
    public Guid? ReceiverId { get; set; }

    // public EmailTemplate? EmailTemplate { get; set; }
}
