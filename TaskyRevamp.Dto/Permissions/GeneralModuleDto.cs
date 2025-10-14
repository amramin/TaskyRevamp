using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Permissions
{
	public class GeneralModuleDto
	{
		public Guid Id { get; set; }
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
        public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ar" ? NameArabic : NameEnglish;
        public bool HasView { get; set; }
		public bool HasEdit { get; set; }
		public bool HasAdd { get; set; }
		public bool HasDelete { get; set; }


	}
}
