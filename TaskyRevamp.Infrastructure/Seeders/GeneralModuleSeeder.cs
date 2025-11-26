using TaskyRevamp.Domain.Models.Permissions.GeneralModule;

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
					NameArabic = "الصلاحيات",

					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Task source",
					NameArabic = "مصادر المهام",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Recycle bin",
					NameArabic = "سلة المحذوفات",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Task rejection",
					NameArabic = "رفض المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{

					NameEnglish = "Add task fields",
					NameArabic = "حقول إضافة المهام",

					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{
					NameEnglish = "Default columns",
					NameArabic = "الأعمدة الافتراضية",
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
					NameArabic = "نوع المهام",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "System identity",
					NameArabic = "هوية النظام",
					HasView = true,
					HasEdit = true,
					HasDelete = false,
					HasAdd = false
				},
				new GeneralModule
				{

					NameEnglish = "Task history log",
					NameArabic = "سجل تاريخ المهام",

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
					NameEnglish = "Working days",
					NameArabic = "أيام العمل",
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
					NameEnglish = "Task priority",
					NameArabic = "أولوية المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Task status",
					NameArabic = "حالات المهمة",
					HasView = true,
					HasEdit = true,
					HasDelete = true,
					HasAdd = true
				},
				new GeneralModule
				{
					NameEnglish = "Task views",
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
					HasEdit = false,
					HasDelete = false,
					HasAdd = true
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
