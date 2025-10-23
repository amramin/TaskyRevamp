using TaskyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Infrastructure.Seeders
{
	internal class WorkingDaysSeeder : ISeeder
	{
		public void Seed(EfDbContext context)
		{
			var weekDaysToSeed = Enum.GetValues(typeof(WeekDays))
									.Cast<WeekDays>()
									.Select(day => new WorkingDaysSettings
									{
										Day = day,
										IsActive = !(day == WeekDays.Friday || day == WeekDays.Saturday)
									})
									.ToList();

			var existingDays = context.WorkingDaysSettings
									  .Select(d => d.Day)
									  .ToHashSet();

			foreach (var day in weekDaysToSeed)
			{
				if (!existingDays.Contains(day.Day))
				{
					context.WorkingDaysSettings.Add(day);
				}
			}
			context.SaveChanges();
		}
	}
}
