using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;

namespace TaskyRevamp.Infrastructure.Seeders
{
	public class GeneralModuleSeeder : ISeeder
	{
		public void Seed(EfDbContext context)
		{
			var _modulesToSeed = new[]
			{
				new GeneralModule
				{
					NameEnglish = "Users",
					NameArabic = "المستخدمين",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Departments",
					NameArabic = "الإدارات",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Privileges ",
					NameArabic = "صلاحيات",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Task source",
					NameArabic = "مصدر المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Recycle bin configuration",
					NameArabic = "إعدادات سلة المحذوفات",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Rejection configuration",
					NameArabic = "إعدادات رفض المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Add Task configuration",
					NameArabic = "إضافة إعدادات المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Default columns configuration",
					NameArabic = "إعدادات الأعمدة الافتراضية",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Filter fields",
					NameArabic = "حقول المرشح",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Task type",
					NameArabic = "نوع المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "System Identity",
					NameArabic = "هوية النظام",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Task History log",
					NameArabic = "سجل تاريخ المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Email notification",
					NameArabic = "إشعارات البريد الإلكتروني",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "System notification",
					NameArabic = "إشعارات النظام",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Working days configuration",
					NameArabic = "إعدادات أيام العمل",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Weekly report",
					NameArabic = "التقرير الإسبوعي",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Priority",
					NameArabic = "الأولوية",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Status",
					NameArabic = "الحالة",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Status trigger",
					NameArabic = "حالة التأهب",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Tasks views",
					NameArabic = "عرض المهام",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Default view",
					NameArabic = "العرض الافتراضي",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Subtask levels",
					NameArabic = "مستوى المهمة الفرعية",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Escalation",
					NameArabic = "التصعيد",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Delegation",
					NameArabic = "التفويض",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
			};

			var existingEn = context.GeneralModule
			.Select(g => g.NameEnglish)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var existingAr = context.GeneralModule
				.Select(g => g.NameArabic)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			foreach (var module in _modulesToSeed)
			{
				if (!existingEn.Contains(module.NameEnglish) && !existingAr.Contains(module.NameArabic))
				{
					context.GeneralModule.Add(module);
				}
			}

			context.SaveChanges();
		}
	}
}
