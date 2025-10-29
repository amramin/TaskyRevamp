using TaskyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public class DefaultColumnsSettingSeeder : ISeeder
    {
        public void Seed(EfDbContext context)
        {
            var settingsToSeed = new[]
            {
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Title", NameArabic = "العنوان", IsActive = true, Order = 1 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Priority", NameArabic = "الأولوية", IsActive = true, Order = 2 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Actual Progress", NameArabic = "التقدم الفعلي", IsActive = true, Order = 3 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Source", NameArabic = "المصدر", IsActive = true, Order = 4 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Start Date", NameArabic = "تاريخ البدء", IsActive = true, Order = 5 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "End Date", NameArabic = "تاريخ الإنتهاء", IsActive = true, Order = 6 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Weight", NameArabic = "الوزن", IsActive = true, Order = 7 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned To", NameArabic = "المسند إليه", IsActive = true, Order = 8 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Creator Department", NameArabic = "إدارة المنشئ", IsActive = true, Order = 9 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned To Department", NameArabic = "إدارة المسند إليه", IsActive = true, Order = 10 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Mandatory", NameArabic = "الزامي", IsActive = true, Order = 11 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Status", NameArabic = "الحالة", IsActive = true, Order = 12 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Dependency", NameArabic = "تبعية المهمة", IsActive = true, Order = 13 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Created By", NameArabic = "المنشئ", IsActive = true, Order = 14 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Created By Department", NameArabic = "إدارة المنشئ", IsActive = true, Order = 15 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Duration", NameArabic = "المدة", IsActive = true, Order = 16 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Planned Progress", NameArabic = "التقدم المخطط", IsActive = true, Order = 17 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Creation Date", NameArabic = "تاريخ الإنشاء", IsActive = true, Order = 18 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Type", NameArabic = "النوع", IsActive = true, Order = 19 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Actual Weight", NameArabic = "الوزن الفعلي", IsActive = true, Order = 20 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Planned Weight", NameArabic = "الوزن المخطط", IsActive = true, Order = 21 },
            };

            var existingNames = context.DefaultColumnsSettings
                .Select(p => p.NameEnglish)
                .ToHashSet();

            foreach (var setting in settingsToSeed)
            {
                if (!existingNames.Contains(setting.NameEnglish))
                {
                    context.DefaultColumnsSettings.Add(setting);
                }
            }


            context.SaveChanges();
        }
    }
}
