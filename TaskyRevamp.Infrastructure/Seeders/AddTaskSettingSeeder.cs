using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public class AddTaskSettingSeeder : ISeeder
    {
        public void Seed(EfDbContext context)
        {
            var settingsToSeed = new[]
            {
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Title", NameArabic = "العنوان", IsActive = true, IsMandatory = true, Order = 1 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Description", NameArabic = "الوصف", IsActive = true, IsMandatory = false, Order = 2 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Source", NameArabic = "المصدر", IsActive = true, IsMandatory = true, Order = 3 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Type", NameArabic = "النوع", IsActive = true, IsMandatory = false, Order = 4 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Start Date", NameArabic = "تاريخ البدء", IsActive = true, IsMandatory = true, Order = 5 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "End Date", NameArabic = "تاريخ الإنتهاء", IsActive = true, IsMandatory = true, Order = 6 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Reminder Date", NameArabic = "تاريخ التذكير", IsActive = true, IsMandatory = false, Order = 7 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Priority", NameArabic = "الأولوية", IsActive = true, IsMandatory = true, Order = 8 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Weight", NameArabic = "الوزن", IsActive = true, IsMandatory = true, Order = 9 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Department", NameArabic = "الإدارة", IsActive = true, IsMandatory = true, Order = 10 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned To", NameArabic = "المسند إليه", IsActive = true, IsMandatory = true, Order = 11 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Dependency", NameArabic = "تبعية المهمة", IsActive = true, IsMandatory = false, Order = 12 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "File Upload", NameArabic = "تحميل ملف", IsActive = true, IsMandatory = false, Order = 13 },
                new AddTaskSettings { Id = Guid.NewGuid(), NameEnglish = "Actual Progress", NameArabic = "التقدم الفعلي", IsActive = true, IsMandatory = true, Order = 14 },
            };

            var existingNames = context.AddTaskSettings
                .Select(p => p.NameEnglish)
                .ToHashSet();

            foreach (var setting in settingsToSeed)
            {
                if (!existingNames.Contains(setting.NameEnglish))
                {
                    context.AddTaskSettings.Add(setting);
                }
            }


            context.SaveChanges();
        }
    }
}
