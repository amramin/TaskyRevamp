using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Domain.Models.Permissions
{
	public class GeneralModule : Entity
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public bool HasView { get; set; }
		public bool HasEdit { get; set; }
		public bool HasAdd { get; set; }
		public bool HasDelete { get; set; }

		public GeneralModule()
		{

		}
		public GeneralModule(string nameEnglish, string nameArabic, bool hasView, bool hasEdit, bool hasAdd, bool hasDelete)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
			HasView = hasView;
			HasEdit = hasEdit;
			HasAdd = hasAdd;
			HasDelete = hasDelete;
		}

		public GeneralModuleDto CopyToDto()
		{
			return new GeneralModuleDto
			{
				Id = Id,
				NameEnglish = NameEnglish,
				NameArabic = NameArabic,
				HasView = HasView,
				HasEdit = HasEdit,
				HasAdd = HasAdd,
				HasDelete = HasDelete
			};
		}
	}
}
