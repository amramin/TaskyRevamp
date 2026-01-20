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
                    Id=Guid.Parse("547022EA-EF8C-4FBC-2236-08DE3318A61C"),
                    NameEnglish = "Not started",
                    NameArabic = "لم تبدأ",
                    NameColor = "#CCCCCC",
                    BackgroundColor = "#FAFAFA"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("9843AF9D-1389-4740-B428-08DE3D8A77AB"),
                    NameEnglish = "To do",
                    NameArabic = "قيد الإنتظار",
                    NameColor = "#007BFF",
                    BackgroundColor = "#E5F2FF"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("753404A6-8B18-43F7-2238-08DE3318A61C"),
                    NameEnglish = "In progress",
                    NameArabic = "قيد التنفيذ",
                    NameColor = "#FFC107",
                    BackgroundColor = "#FFF9E6"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("270A78EB-C5CA-475D-2239-08DE3318A61C"),
                    NameEnglish = "Delayed",
                    NameArabic = "متأخرة",
                    NameColor = "#FDEAEA",
                    BackgroundColor = "#E82C2C"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("6EE4574D-C439-45B4-223A-08DE3318A61C"),
                    NameEnglish = "Pending review",
                    NameArabic = "في انتظار المراجعة",
                    NameColor = "#6F42C1",
                    BackgroundColor = "#F1ECF9"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("E1319FC1-8CB8-495C-223B-08DE3318A61C"),
                    NameEnglish = "Reopened",
                    NameArabic = "تم إرجاعها",
                    NameColor = "#FD7E14",
                    BackgroundColor = "#FFF2E7"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C"),
                    NameEnglish = "Completed",
                    NameArabic = "مكتملة",
                    NameColor = "#28A745",
                    BackgroundColor = "#E9F6EC"
                },
                new StatusSettings
                {
                    Id=Guid.Parse("D8E94CCE-586A-46D3-223D-08DE3318A61C"),
                    NameEnglish = "soft-deleted",
                    NameArabic = "مؤرشفة",
                    NameColor = "#979EAB",
                    BackgroundColor = "#F8F9FA"
                },
            };

            var existing = context.StatusSettings
            .Select(s => s.Id)
            ;

            //var existingAr = context.StatusSettings
            //	.Select(s => s.NameArabic)
            //	.ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var status in StatusToSeed)
            {
                if (!existing.Contains(status.Id))
                {
                    context.StatusSettings.Add(status);
                }
            }

            context.SaveChanges();
        }
    }
}
