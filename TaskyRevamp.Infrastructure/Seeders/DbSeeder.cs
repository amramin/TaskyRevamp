using MediatR;
using TaskyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Infrastructure.Seeders
{
    public static class DbSeeder
    {
        public static void Seed(EfDbContext context)
        {
            var seeders = new List<ISeeder>
            {
                new PrioritySeeder(),
                new StatusSeeder(),
                new ViewTaskListSeeder(),
                new SubTaskLevelSeeder(),
                new WorkingDaysSeeder(),
                new DefaultColumnsSettingSeeder(),
                new FilterFieldsSettingSeeder(),
                new AddTaskSettingSeeder(),
                new GeneralModuleSeeder(),
                new ReportModuleSeeder()
            };

            foreach (var seeder in seeders)
                seeder.Seed(context);

        }
    }
}
