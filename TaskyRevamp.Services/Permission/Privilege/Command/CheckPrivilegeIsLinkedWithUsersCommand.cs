using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using Privileges = TaskyRevamp.Domain.Models.Permissions.Privilege;

namespace TaskyRevamp.Services.Permission.Privilege.Command
{
	public record CheckPrivilegeIsLinkedWithUsersCommand(Guid PrivilegeId) : IRequest<bool>;
	public class CheckPrivilegeIsLinkedWithUsersHandler : IRequestHandler<CheckPrivilegeIsLinkedWithUsersCommand, bool>
	{
		private readonly IRepository<Privileges> _privilegeRepository;
		private readonly IRepository<User> _userRepository;

		public CheckPrivilegeIsLinkedWithUsersHandler(IRepository<Privileges> privilegeRepository, IRepository<User> userRepository)
		{
			_privilegeRepository = privilegeRepository;
			_userRepository = userRepository;
		}
		public async Task<bool> Handle(CheckPrivilegeIsLinkedWithUsersCommand request, CancellationToken cancellationToken)
		{
			var usersWithPrivilege = await _userRepository.AllAsNoTracking();
			if(usersWithPrivilege != null && usersWithPrivilege.Success && usersWithPrivilege.Value != null)
			{
				foreach (var user in usersWithPrivilege.Value)
				{
					if (user.PrivilegeId != null && user.PrivilegeId != request.PrivilegeId)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
