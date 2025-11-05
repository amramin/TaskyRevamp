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

            var assembly = typeof(SystemIdentitySeeder).Assembly;
            using var stream = assembly.GetManifestResourceStream("TaskyRevamp.Infrastructure.Seeders.Resources.stingray-logo.svg");
            if (stream == null)
                throw new FileNotFoundException("Embedded resource stingray-logo.svg not found.");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            byte[] logoBytes = ms.ToArray();

            var systemIdentityToSeed = new SystemIdentity
			{
				PrimaryColor = "#1740A5",
				PrimaryActiveColor = "#2F53AE",
				MainTitle = "#000",
				SubTitle = "#000",
				NavigationBackground = "#DEE6F6",
				BorderColor = "#E0E0E0",
				Logo = logoBytes,
				NameArabic = "",
				NameEnglish = "",
            };
			context.SystemIdentity.Add(systemIdentityToSeed);
			context.SaveChanges();
			
		}
	}
}
