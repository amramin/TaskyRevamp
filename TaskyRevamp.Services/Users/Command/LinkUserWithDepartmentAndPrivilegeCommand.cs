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
using TaskyRevamp.Dto.Account;

namespace TaskyRevamp.Services.Users.Command
{
	public record LinkUserWithDepartmentAndPrivilegeCommand(List<UserLinkDto> UserLinkDtos) : IRequest<bool>;
	public class LinkUserWithDepartmentAndPrivilegeHandler : IRequestHandler<LinkUserWithDepartmentAndPrivilegeCommand, bool>
	{
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Department> _departmentRepository;
		private readonly IRepository<Privilege> _privilegeRepository;

		public LinkUserWithDepartmentAndPrivilegeHandler(IRepository<User> userRepository, IRepository<Department> departmentRepository, IRepository<Privilege> privilegeRepository)
		{
			_userRepository = userRepository;
			_departmentRepository = departmentRepository;
			_privilegeRepository = privilegeRepository;
		}

		public async Task<bool> Handle(LinkUserWithDepartmentAndPrivilegeCommand request, CancellationToken cancellationToken)
		{
			foreach(var UserLinkDto in request.UserLinkDtos)
			{
				var email = UserLinkDto.email.Trim().ToLower();
				var _departmentName = UserLinkDto.departmentName.Trim().ToLower();
				var _privilegeName = UserLinkDto.privilegeName.Trim().ToLower();

				var deptRes = await _departmentRepository.FindBy(d => d.NameEnglish.ToLower() == _departmentName || d.NameArabic.ToLower() == _departmentName);
				var department = deptRes.Value?.FirstOrDefault();
				if (!deptRes.Success || department == null)
					return false;

				var privRes = await _privilegeRepository.FindBy(p => p.NameEnglish.ToLower() == _privilegeName || p.NameArabic.ToLower() == _privilegeName);
				var privilege = privRes.Value?.FirstOrDefault();
				if (!privRes.Success || privilege == null)
					return false;

				var userRes = await _userRepository.FindBy(u => u.Email!.ToLower() == email);
				var user = userRes.Value?.FirstOrDefault();
				if (!userRes.Success || user == null)
				{
					return false;
				}

				bool hasDepartment = user.DepartmentId != null;
				bool hasPrivilege = user.PrivilegeId != null;

				if (hasDepartment && hasPrivilege)
					continue;

				if (hasDepartment && !hasPrivilege)
				{
					user.PrivilegeId = privilege.Id;
					await _userRepository.Update(user);
					continue;
				}

				if (!hasDepartment && hasPrivilege)
				{
					user.DepartmentId = department.Id;
					await _userRepository.Update(user);
					continue;
				}

				if (!hasDepartment && !hasPrivilege)
				{
					user.DepartmentId = department.Id;
					user.PrivilegeId = privilege.Id;
					await _userRepository.Update(user);
					continue;
				}
			}
			await _userRepository.SaveChangesAsync();
			return true;
		}
	}
}
