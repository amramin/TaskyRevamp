using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Account
{
	public class AdUser
	{
		public string Username { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string DistinguishedName { get; set; }
		public string GivenName { get; set; }
		public string Surname { get; set; }
		public string Title { get; set; }
		public string Phone { get; set; }
		public bool IsActive { get; set; }
		public string ManagerUsername { get; set; }
		public bool IsAuthenticated { get; set; }
	}
}
