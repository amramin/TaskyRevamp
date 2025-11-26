using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Command
{
	public record DeletePrivilegeCommand(Guid Id) : IRequest<bool>;
	public class DeletePrivilegeHandler : IRequestHandler<DeletePrivilegeCommand, bool>
	{
		private readonly IRepository<Privileges> _privilegeRepository;

		public DeletePrivilegeHandler(IRepository<Privileges> privilegeRepository)
		{
			_privilegeRepository = privilegeRepository;
		}
		public async Task<bool> Handle(DeletePrivilegeCommand request, CancellationToken cancellationToken)
		{
			var existingPrivilege = await _privilegeRepository.FindByKey(request.Id);
			if (existingPrivilege != null && existingPrivilege.Success && existingPrivilege.Value != null)
			{
				await _privilegeRepository.Delete(existingPrivilege.Value.Id);
			}
			return true;
		}
	}
}
