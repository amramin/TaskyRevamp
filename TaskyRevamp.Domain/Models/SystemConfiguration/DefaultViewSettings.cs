using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class DefaultViewSettings : Entity
	{
		public int DefaultSelected { get; set; }
		public int SubTaskLevels { get; set; }

		public DefaultViewSettings()
		{
			
		}
		public DefaultViewSettings(int defaultSelected, int subTaskLevels)
		{
			DefaultSelected = defaultSelected;
			SubTaskLevels = subTaskLevels;
		}

		public DefaultViewSettingsDto CopyToDto()
		{
			return new DefaultViewSettingsDto
			{
				Id = Id,
				DefaultSelected = DefaultSelected,
				SubTaskLevels = SubTaskLevels
			};
		}
	}
}
