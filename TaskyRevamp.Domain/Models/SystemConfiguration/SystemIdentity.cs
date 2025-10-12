using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class SystemIdentity : Entity
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public string PrimaryColor { get; set; }
		public string PrimaryActiveColor { get; set; }
		public string MainTitle { get; set; }
		public string SubTitle { get; set; }
		public string NavigationBackground { get; set; }
		public string BorderColor { get; set; }
		public byte[] Logo { get; set; }

		public SystemIdentity()
		{
			
		}
		public SystemIdentity(string nameEnglish, string nameArabic, string primaryColor, string primaryActiveColor, string mainTitle, string subTitle, string navigationBackground, string borderColor, byte[] logo)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
			PrimaryColor = primaryColor;
			PrimaryActiveColor = primaryActiveColor;
			MainTitle = mainTitle;
			SubTitle = subTitle;
			NavigationBackground = navigationBackground;
			BorderColor = borderColor;
			Logo = logo;
		}

		public SystemIdentityDto CopyToDto()
		{
			return new SystemIdentityDto
			{
				Id = Id,
				NameEnglish = NameEnglish,
				NameArabic = NameArabic,
				PrimaryColor = PrimaryColor,
				PrimaryActiveColor = PrimaryActiveColor,
				MainTitle = MainTitle,
				SubTitle = SubTitle,
				NavigationBackground = NavigationBackground,
				BorderColor = BorderColor,
				Logo = Logo
			};
		}
	}
}
