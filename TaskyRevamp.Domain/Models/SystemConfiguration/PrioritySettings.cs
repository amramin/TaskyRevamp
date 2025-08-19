using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class PrioritySettings : Entity
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public string NameColor { get; set; }
		public string BackgroundColor { get;  set; }
		public int Order { get; set; }

		public PrioritySettings()
		{

		}

		public PrioritySettings(string nameEnglish, string nameArabic, string nameColor, string backgroundColor, int order)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
			NameColor = nameColor;
			BackgroundColor = backgroundColor;
			Order = order;
		}

		//public void update()
		//{

		//}
		 public PriorityDto CopyToDto()
		{
			return new PriorityDto
			{
				Id = Id,
				NameEnglish = NameEnglish,
				NameArabic = NameArabic,
				NameColor = NameColor,
				BackgroundColor = BackgroundColor,
				Order = Order
			};
		}
	}
}
