using TaskyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Infrastructure.Seeders
{
	public class StatusSeeder : ISeeder
	{
		public void Seed(EfDbContext context)
		{
			var StatusToSeed = new[]
			{
				new StatusSettings
				{
					NameEnglish = "Not Started",
					NameArabic = "لم تبدأ",
					NameColor = "#CCCCCC",
					BackgroundColor = "#FAFAFA"
				},
				new StatusSettings
				{
					NameEnglish = "To Do",
					NameArabic = "قيد الإنتظار",
					NameColor = "#007BFF",
					BackgroundColor = "#E5F2FF"
				},
				new StatusSettings
				{
					NameEnglish = "In Progress",
					NameArabic = "قيد التنفيذ",
					NameColor = "#FFC107",
					BackgroundColor = "#FFF9E6"
				},
				new StatusSettings
				{
					NameEnglish = "Delayed",
					NameArabic = "متأخرة",
					NameColor = "#FDEAEA",
					BackgroundColor = "#E82C2C"
				},
				new StatusSettings
				{
					NameEnglish = "Pending Review",
					NameArabic = "في انتظار المراجعة",
					NameColor = "#6F42C1",
					BackgroundColor = "#F1ECF9"
				},
				new StatusSettings
				{
					NameEnglish = "Reopened",
					NameArabic = "تم إرجاعها",
					NameColor = "#FD7E14",
					BackgroundColor = "#FFF2E7"
				},
				new StatusSettings
				{
					NameEnglish = "Completed",
					NameArabic = "مكتملة",
					NameColor = "#28A745",
					BackgroundColor = "#E9F6EC"
				},
				new StatusSettings
				{
					NameEnglish = "soft-deleted",
					NameArabic = "مؤرشفة",
					NameColor = "#979EAB",
					BackgroundColor = "#F8F9FA"
				},
			};

			var existingEn = context.StatusSettings
			.Select(s => s.NameEnglish)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var existingAr = context.StatusSettings
				.Select(s => s.NameArabic)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			foreach (var status in StatusToSeed)
			{
				if (!existingEn.Contains(status.NameEnglish) && !existingAr.Contains(status.NameArabic))
				{
					context.StatusSettings.Add(status);
				}
			}

			context.SaveChanges();
		}
	}
}
