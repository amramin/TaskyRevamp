using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Account
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string userNameAR { get; set; }
        public string userNameEN { get; set; }
        public string UserName => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? userNameAR : userNameEN;

        public string Email { get; set; }
    }
}
