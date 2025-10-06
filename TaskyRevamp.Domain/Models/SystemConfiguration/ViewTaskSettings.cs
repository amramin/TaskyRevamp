using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class ViewTaskSettings : Entity
	{
		public ViewType ViewType { get; set; }
		public bool IsActive { get; set; }

		public ViewTaskSettings()
		{

		}

		public ViewTaskSettings(ViewType viewType, bool isActive)
		{
			ViewType =	viewType;
			IsActive = isActive;
		}
		public ViewTaskSettingsDto CopyToDto()
		{
			return new ViewTaskSettingsDto
			{
				Id = Id,
				ViewType = ViewType,
				IsActive = IsActive
			};

		}
	}
}
