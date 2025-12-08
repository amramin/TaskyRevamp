using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Email;

public class MailSettings
{
    public string Server { set; get; }
    public int Port { set; get; }
    public string SenderName { set; get; }
    public string SenderEmail { set; get; }
    public string UserName { set; get; }
    public string Password { set; get; }
}