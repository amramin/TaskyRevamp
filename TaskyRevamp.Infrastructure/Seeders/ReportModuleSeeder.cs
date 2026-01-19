using TaskyRevamp.Domain.Models.Permissions.ReportModule;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public class ReportModuleSeeder : ISeeder
    {
        public void Seed(EfDbContext context)
        {
            if (context.ReportModule.Any())
                return;

            var modulesToSeed = new[]
            {
                new ReportModule
                {
                    Id = Guid.Parse("d22b7d4f-b326-439d-8499-032cb5abe3ca"),
                    NameEnglish = "User tasks",
                    NameArabic = "مهام المستخدم",
                    HintEnglish = "The report will include only the user tasks.",
                    HintArabic = "سيتضمن التقرير مهام المستخدم فقط"
                },
                new ReportModule
                {
                    Id = Guid.Parse("c8728a39-e0df-46e3-ac2c-150858d7e198"),
                    NameEnglish = "Without manager tasks",
                    NameArabic = "مهام إدارة المستخدم بدون مهام المدير",
                    HintEnglish = "The report includes all tasks of the user department but excludes any tasks related to managers of the user department.",
                    HintArabic = "يتضمن التقرير جميع مهام إدارة المستخدم، لكنه يستبعد أي مهام مرتبطة بمديري إدارة المستخدم."
                },
                new ReportModule
                {
                    Id = Guid.Parse("8350dbf5-b256-4390-90c3-3eca764b6603"),
                    NameEnglish = "User department tasks with manager tasks",
                    NameArabic = "مهام إدارة المستخدم مع مهام المدير",
                    HintEnglish = "The report includes all tasks of the user department, with all tasks related to the managers of the user department.",
                    HintArabic = "يتضمن التقرير جميع مهام إدارة المستخدم، مع جميع المهام المتعلقة بمديري إدارة المستخدمين."
                },
                new ReportModule
                {
                    Id = Guid.Parse("c752d455-6db0-4bff-9936-b4d0566c54cd"),
                    NameEnglish = "User tasks and other viewed tasks",
                    NameArabic = "مهام المستخدم والمهام الأخرى التي تمت مشاهدتها",
                    HintEnglish = "The report will include the user tasks including the team tasks that he is able to view.",
                    HintArabic = "سيتضمن التقرير مهام المستخدم بما في ذلك مهام الفريق التي يتمكن من مشاهدتها"
                },
                new ReportModule
                {
                    Id = Guid.Parse("5554cbe3-9929-4d87-bab9-d91eeb5c030e"),
                    NameEnglish = "User department tasks with all sub-department tasks, including manager tasks",
                    NameArabic = "مهام إدارة المستخدم مع جميع مهام الإدارات الفرعية، بما في ذلك مهام المديرين",
                    HintEnglish = "The report includes all tasks of the user department and tasks of all sub-department levels, with all tasks related to the managers of the user department.",
                    HintArabic = "يتضمن التقرير جميع مهام إدارة المستخدم ومهام جميع إدارة الأقسام الفرعية، مع جميع المهام المتعلقة بمديري إدارة المستخدم"
                }
            };

            foreach (var module in modulesToSeed)
            {
                context.ReportModule.Add(module);
            }

            context.SaveChanges();

        }
    }
}
