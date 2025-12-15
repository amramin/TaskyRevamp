using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Query
{
	public record GetPrivilegeNameByIdQuery(Guid id) : IRequest<string>;
	public class GetPrivilegeNameByIdHandler : IRequestHandler<GetPrivilegeNameByIdQuery, string>
	{
		private readonly IRepository<Privileges> _privilegesRepository;

		public GetPrivilegeNameByIdHandler(IRepository<Privileges> privilegesRepository)
		{
			_privilegesRepository = privilegesRepository;
		}

		public async Task<string> Handle(GetPrivilegeNameByIdQuery request, CancellationToken cancellationToken)
		{
			string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			string privilegeName = "";
			var res = await _privilegesRepository.FindByKey(request.id);
			if(res.Success && res.Value != null)
			{
				var privilege = res.Value;
				privilegeName = (currentCulture == "ar") ? privilege.NameArabic : privilege.NameEnglish; 
			}
			return privilegeName;
		}
	}
}
