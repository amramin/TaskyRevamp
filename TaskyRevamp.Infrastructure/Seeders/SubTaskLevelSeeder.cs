
using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;

namespace TaskyRevamp.Infrastructure.Seeders
{
	public class SubTaskLevelSeeder : ISeeder
	{
		public void Seed(EfDbContext context)
		{
			var existingSubTask = context.DefaultViewSettings.Any();

			if (!existingSubTask)
			{
				context.DefaultViewSettings.Add(new DefaultViewSettings
				{
					SubTaskLevels = 3 
				});
				context.SaveChanges();
			}
		}
	}
}
