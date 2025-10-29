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
                    Id = Guid.NewGuid(),
                    NameEnglish = "User tasks",
                    NameArabic = "مهام المستخدم",
                    HintEnglish = "The report will include only the user tasks.",
                    HintArabic = "سيتضمن التقرير مهام المستخدم فقط"
                },
                new ReportModule
                {
                    Id = Guid.NewGuid(),
                    NameEnglish = "Without manager tasks",
                    NameArabic = "مهام إدارة المستخدم بدون مهام المدير",
                    HintEnglish = "The report includes all tasks of the user department but excludes any tasks related to managers of the user department.",
                    HintArabic = "يتضمن التقرير جميع مهام إدارة المستخدم، لكنه يستبعد أي مهام مرتبطة بمديري إدارة المستخدم."
                },
                new ReportModule
                {
                    Id = Guid.NewGuid(),
                    NameEnglish = "User Department tasks with manager tasks",
                    NameArabic = "مهام إدارة المستخدم مع مهام المدير",
                    HintEnglish = "The report includes all tasks of the user department, with all tasks related to the managers of the user department.",
                    HintArabic = "يتضمن التقرير جميع مهام إدارة المستخدم، مع جميع المهام المتعلقة بمديري إدارة المستخدمين."
                },
                new ReportModule
                {
                    Id = Guid.NewGuid(),
                    NameEnglish = "User tasks and other viewed tasks",
                    NameArabic = "مهام المستخدم والمهام الأخرى التي تمت مشاهدتها",
                    HintEnglish = "The report will include the user tasks including the team tasks that he is able to view.",
                    HintArabic = "سيتضمن التقرير مهام المستخدم بما في ذلك مهام الفريق التي يتمكن من مشاهدتها"
                },
                new ReportModule
                {
                    Id = Guid.NewGuid(),
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
