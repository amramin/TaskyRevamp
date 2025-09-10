using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public class FilterFieldsSettingSeeder : ISeeder
    {
        public void Seed(EfDbContext context)
        {
            var settingsToSeed = new[]
            {
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Title", NameArabic = "العنوان", IsActive = true, Order = 1 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Priority", NameArabic = "الأولوية", IsActive = true, Order = 2 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Status", NameArabic = "الحالة", IsActive = true, Order = 3 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "From Start Date", NameArabic = "من تاريخ البدء", IsActive = true, Order = 4 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "To Start Date", NameArabic = "إلى تاريخ البدء", IsActive = true, Order = 5 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "From End Date", NameArabic = "من تاريخ الإنتهاء", IsActive = true, Order = 6 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "To End Date", NameArabic = "إلى تاريخ الإنتهاء", IsActive = true, Order = 7 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned To", NameArabic = "المسند إليه", IsActive = true, Order = 8 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Assigned To Department", NameArabic = "إدارة المسند إليه", IsActive = true, Order = 9 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Created By", NameArabic = "المنشئ", IsActive = true, Order = 10 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Created By Department", NameArabic = "إدارة المنشئ", IsActive = true, Order = 11 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "From Creation Date", NameArabic = "من تاريخ الإنشاء", IsActive = true, Order = 12 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "To Creation Date", NameArabic = "إلى تاريخ الإنشاء", IsActive = true, Order = 13 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Type", NameArabic = "النوع", IsActive = true, Order = 14 },
                new FilterFieldsSettings { Id = Guid.NewGuid(), NameEnglish = "Source", NameArabic = "المصدر", IsActive = true, Order = 15 },
            };

            var existingNames = context.FilterFieldsSettings
                .Select(p => p.NameEnglish)
                .ToHashSet();

            foreach (var setting in settingsToSeed)
            {
                if (!existingNames.Contains(setting.NameEnglish))
                {
                    context.FilterFieldsSettings.Add(setting);
                }
            }

            context.SaveChanges();
        }
    }
}
