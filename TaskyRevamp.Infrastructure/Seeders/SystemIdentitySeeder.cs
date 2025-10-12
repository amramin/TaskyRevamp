using SurveyRevamp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Infrastructure.Migrations;
using static System.Net.Mime.MediaTypeNames;

namespace TaskyRevamp.Infrastructure.Seeders
{
	public class SystemIdentitySeeder : ISeeder
	{

		public void Seed(EfDbContext context)
		{
			if (context.SystemIdentity.Any())
				return;


			//var logoPath = "images/stingray-logo.svg";

			//if (!File.Exists(logoPath))
			//	throw new FileNotFoundException($"Default logo not found at {logoPath}");

			//var logoBytes = File.ReadAllBytes(logoPath);

			var systemIdentityToSeed = new SystemIdentity
			{
				PrimaryColor = "#1740A5",
				PrimaryActiveColor = "#2F53AE",
				MainTitle = "#000",
				SubTitle = "#000",
				NavigationBackground = "#DEE6F6",
				BorderColor = "#E0E0E0",
				//Logo = logoBytes
			};
			context.SystemIdentity.Add(systemIdentityToSeed);
			context.SaveChanges();
			
		}
	}
}
