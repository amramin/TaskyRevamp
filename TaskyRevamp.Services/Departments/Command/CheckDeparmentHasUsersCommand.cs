using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Departments.Command
{
	public record CheckDeparmentHasUsersCommand(Guid dpartmentId) : IRequest<bool>;
	public class CheckDeparmentHasUsersHandler : IRequestHandler<CheckDeparmentHasUsersCommand, bool>
	{
		private readonly IRepository<User> _userRepository;

		public CheckDeparmentHasUsersHandler(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}
		public async Task<bool> Handle(CheckDeparmentHasUsersCommand request, CancellationToken cancellationToken)
		{
			var usersWithDepartment = await _userRepository.FindBy(u => u.DepartmentId == request.dpartmentId);
			if (usersWithDepartment != null && usersWithDepartment.Success && usersWithDepartment.Value != null)
			{
				if(usersWithDepartment.Value.Any())
					return true;
			}
			return false;
		}
	}
}
