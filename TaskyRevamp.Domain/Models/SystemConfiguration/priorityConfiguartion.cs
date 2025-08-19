using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class priorityConfiguartion : Entity
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public string NameColor { get; set; }
		public string BackgroundColor { get;  set; }
		public int Ranking { get; set; }
		public int Order { get; set; }

		public priorityConfiguartion()
		{

		}

		public priorityConfiguartion(string nameEnglish, string nameArabic, string nameColor, string backgroundColor, int ranking, int order)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
			NameColor = nameColor;
			BackgroundColor = backgroundColor;
			Ranking = ranking;
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
				Ranking = Ranking,
				Order = Order
			};
		}
	}
}
