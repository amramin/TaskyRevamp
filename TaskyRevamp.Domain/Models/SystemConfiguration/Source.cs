using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Domain.Models.SystemConfiguration
{
	public class Source : Entity, IHasCreationMetaData, IHasUpdateMetaData
	{
		public string NameEnglish { get; set; }
		public string NameArabic { get; set; }
		public bool IsActive { get; set; }
		public User CreatedBy { get; set; }
		public Guid CreatedById { get; set; }
		public DateTime CreateDate { get; set; }
		public User? UpdatedBy { get; set; }
		public Guid? UpdatedById { get; set; }
		public DateTime? UpdateDate { get; set; }

		public Source()
		{

		}
		public Source(string nameEnglish, string nameArabic, string descriptionEnglish, string descriptionArabic, bool isActive)
		{
			NameEnglish = nameEnglish;
			NameArabic = nameArabic;
			IsActive = isActive;
		}

		public SourceDto CopyToDto()
		{
			return new SourceDto
			{
				Id = Id,
				NameEnglish = NameEnglish,
				NameArabic = NameArabic,
				IsActive = IsActive,
				CreatedById = CreatedById,
				CreateDate = CreateDate,
				UpdatedById = UpdatedById,
				UpdateDate = UpdateDate,
			};
		}
	}
}
