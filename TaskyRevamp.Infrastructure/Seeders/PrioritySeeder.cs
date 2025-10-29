using TaskyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public class PrioritySeeder : ISeeder
    {
        public void Seed(EfDbContext context)
        {
            var prioritiesToSeed = new[]
            {
                new PrioritySettings{
                   NameEnglish = "Critical",
                   NameArabic = "حرجة",
                   NameColor = "#FDEAEA",
                   BackgroundColor = "#E82C2C",
                   Order = 1
               },
               new PrioritySettings{
                   NameEnglish = "High",
                   NameArabic = "عالية",
                   NameColor = "#FFA000",
                   BackgroundColor = "#FCF6ED",
                   Order = 2
               },
               new PrioritySettings{
                   NameEnglish = "Meduim",
                   NameArabic = "متوسطة",
                   NameColor = "#E6A700",
                   BackgroundColor = "#FCF6E5",
                   Order = 3
               },
               new PrioritySettings{
                   NameEnglish = "Low",
                   NameArabic = "عادية",
                   NameColor = "#979EAB",
                   BackgroundColor = "#F8F9FF",
                   Order = 4
               }
            };

            var existingNames = context.PrioritySettings
                .Select(p => p.NameEnglish)
                .ToHashSet();

            foreach (var priority in prioritiesToSeed)
            {
                if (!existingNames.Contains(priority.NameEnglish))
                {
                    context.PrioritySettings.Add(priority);
                }
            }


            context.SaveChanges();
        }
    }
}
