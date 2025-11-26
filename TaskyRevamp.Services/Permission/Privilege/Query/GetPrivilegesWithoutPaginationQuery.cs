using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Query
{
	public record GetPrivilegesWithoutPaginationQuery() : IRequest<List<PrivilegeDto>>;
	public class GetPrivilegesWithoutPaginationHndler : IRequestHandler<GetPrivilegesWithoutPaginationQuery, List<PrivilegeDto>>
	{
		private readonly IRepository<Privileges> _privilegeRepository;

		public GetPrivilegesWithoutPaginationHndler(IRepository<Privileges> privilegeRepository)
		{
			_privilegeRepository = privilegeRepository;
		}

		public async Task<List<PrivilegeDto>> Handle(GetPrivilegesWithoutPaginationQuery request, CancellationToken cancellationToken)
		{
			List<PrivilegeDto> privilegeDtos = new();
			var res = await _privilegeRepository.AllAsNoTracking();
			if(res.Success && res.Value != null && res.Value.Any())
			{
				privilegeDtos = res.Value.Select(p => p.CopyToDto()).ToList();
			}

			return privilegeDtos;
		}
	}
}
