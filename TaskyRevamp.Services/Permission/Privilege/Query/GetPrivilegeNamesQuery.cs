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
	public record GetPrivilegeNamesQuery() : IRequest<List<string>>;
	public class GetPrivilegeNamesHandler : IRequestHandler<GetPrivilegeNamesQuery, List<string>>
	{
		private readonly IRepository<Privileges> _privilegeRepository;

		public GetPrivilegeNamesHandler(IRepository<Privileges> privilegeRepository)
		{
			_privilegeRepository = privilegeRepository;
		}
		public async Task<List<string>> Handle(GetPrivilegeNamesQuery request, CancellationToken cancellationToken)
		{
			List<string> privilegeNames = new List<string>();
			var res = await _privilegeRepository.AllAsNoTracking();
			if (res.Success && res.Value != null)
			{
				privilegeNames = res.Value
					.Select(d => d.NameEnglish.Trim())
					.Concat(res.Value.Select(d => d.NameArabic.Trim()))
					.ToHashSet(StringComparer.OrdinalIgnoreCase)
					.ToList();

			}
			return privilegeNames;
		}
	}
}
