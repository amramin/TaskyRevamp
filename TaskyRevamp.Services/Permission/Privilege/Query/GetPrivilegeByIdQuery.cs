using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Permissions;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Query
{
	public record GetPrivilegeByIdQuery(Guid Id) : IRequest<PrivilegeDto>;
	public class GetPrivilegeByIdHandler : IRequestHandler<GetPrivilegeByIdQuery, PrivilegeDto>
	{
		private readonly IRepository<Privileges> _privilegesRepository;
		public GetPrivilegeByIdHandler(IRepository<Privileges> privilegesRepository)
		{
			_privilegesRepository = privilegesRepository;
		}
		public async Task<PrivilegeDto> Handle(GetPrivilegeByIdQuery request, CancellationToken cancellationToken)
		{
			PrivilegeDto _privilegeDto = new PrivilegeDto();
			var res = await _privilegesRepository.FindByKey(request.Id);
			if (res.Success && res.Value != null && res != null)
			{
				_privilegeDto = res.Value.CopyToDto();
			}
			return _privilegeDto;
		}
	}
}
