using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Dto.SystemConfiguration
{
	public class ViewTaskSettingsDto
	{
		public Guid Id { get; set; }
		public ViewType ViewType { get; set; }
		public bool IsActive { get; set; }
	}
}
