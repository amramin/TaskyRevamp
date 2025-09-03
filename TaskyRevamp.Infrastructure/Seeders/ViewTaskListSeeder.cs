using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.Enums;

namespace TaskyRevamp.Infrastructure.Seeders
{
	public class ViewTaskListSeeder : ISeeder
	{
		public void Seed(EfDbContext context)
		{
			var ViewTaskListToSeed = Enum.GetValues(typeof(ViewType)).Cast<ViewType>()
										.Select(v => new ViewTaskSettings
										{
											ViewType = v,
											IsActive = true
										}).ToList();

			var existingTypes = context.ViewTaskSettings
									  .Select(v => v.ViewType)
									  .ToHashSet();

			foreach (var type in ViewTaskListToSeed)
			{
				if (!existingTypes.Contains(type.ViewType))
				{
					context.ViewTaskSettings.Add(type);
				}
			}
			context.SaveChanges();
		}
	}
}
