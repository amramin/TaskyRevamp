using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Departments.Command
{
	public record CheckDeparmentHasUsersCommand(Guid dpartmentId) : IRequest<bool>;
	public class CheckDeparmentHasUsersHandler : IRequestHandler<CheckDeparmentHasUsersCommand, bool>
	{
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Department> _departmentRepository;
		public CheckDeparmentHasUsersHandler(IRepository<User> userRepository, IRepository<Department> departmentRepository)
		{
			_userRepository = userRepository;
			_departmentRepository = departmentRepository;
		}
		public async Task<bool> Handle(CheckDeparmentHasUsersCommand request, CancellationToken cancellationToken)
		{
			var usersWithDepartment = await _userRepository.FindBy(u => u.DepartmentId == request.dpartmentId);
			var subdepartment = await _departmentRepository.FindBy(d => d.ParentdepartmentId == request.dpartmentId);
			if ((usersWithDepartment != null && usersWithDepartment.Success && usersWithDepartment.Value != null)||
                (subdepartment != null && subdepartment.Success && subdepartment.Value != null))
			{
				if(usersWithDepartment.Value.Any()|| subdepartment.Value.Any())
					return true;
			}
			return false;
		}
	}
}
