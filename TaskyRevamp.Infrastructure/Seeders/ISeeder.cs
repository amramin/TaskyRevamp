using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Infrastructure.Seeders
{
	internal interface ISeeder
	{
		void Seed(EfDbContext context);
	}
}
