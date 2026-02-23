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
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Title", NameArabic = "العنوان", IsActive = true,IsDisplay=true, Order = 1 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Priority", NameArabic = "الأولوية", IsActive = true,IsDisplay=true, Order = 2 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Actual progress", NameArabic = "التقدم الفعلي", IsActive = true,IsDisplay=true, Order = 3 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Source", NameArabic = "المصدر", IsActive = true,IsDisplay=false, Order = 4 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Start date", NameArabic = "تاريخ البدء", IsActive = true,IsDisplay=true, Order = 5 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "End date", NameArabic = "تاريخ الإنتهاء", IsActive = true,IsDisplay=true, Order = 6 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Weight", NameArabic = "الوزن", IsActive = true,IsDisplay=false, Order = 7 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned to", NameArabic = "المسند إليه", IsActive = true,IsDisplay=true, Order = 8 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned to department", NameArabic = "إدارة المسند إليه", IsActive = true,IsDisplay=false, Order = 9 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Status", NameArabic = "الحالة", IsActive = true,IsDisplay=true, Order = 10 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Dependency", NameArabic = "تبعية المهمة", IsActive = true,IsDisplay=false, Order = 11 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Created by", NameArabic = "المنشئ", IsActive = true,IsDisplay=false, Order = 12 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Created by department", NameArabic = "إدارة المنشئ", IsActive = true,IsDisplay=false, Order = 13 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Duration", NameArabic = "المدة", IsActive = true,IsDisplay=false, Order = 14 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Planned progress", NameArabic = "التقدم المخطط", IsActive = true,IsDisplay=false, Order = 15 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Creation date", NameArabic = "تاريخ الإنشاء", IsActive = true,IsDisplay=false, Order = 16 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Type", NameArabic = "النوع", IsActive = true,IsDisplay=false, Order = 17 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Actual weight", NameArabic = "الوزن الفعلي", IsActive = true,IsDisplay=false, Order = 18 },
                new DefaultColumnsSettings { Id = Guid.NewGuid(), NameEnglish = "Planned weight", NameArabic = "الوزن المخطط", IsActive = true,IsDisplay=false, Order = 19 },
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
