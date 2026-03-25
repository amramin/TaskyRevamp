using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Account
{
    public class UserStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InActiveUsers { get; set; }
        public int LinkedWithTasksUsers { get; set; }
        public int LoggedInUsers { get; set; }
        public int LinkedWithPrivilagesUsers { get; set; }

    }
}
