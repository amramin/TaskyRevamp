using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Permissions.GeneralModule;

namespace TaskyRevamp.Domain.Models.Permissions.GeneralModule
{
	public class GeneralModulePermission : Entity
	{
		public Guid PrivilegeId { get; set; }
		public Privilege Privilege { get; set; }
		public Guid GeneralModuleId { get; set; }
		public GeneralModule GeneralModule { get; set; }
		public bool IsView { get; set; }
		public bool IsEdit { get; set; }
		public bool IsAdd { get; set; }
		public bool IsDelete { get; set; }
		public List<int>? DelegationFromUser { get; set; }
		public DelegationToUser? DelegationToUser { get; set; }
		public List<Guid>? DelegationFromUserDepartments { get; set; }
		public List<Guid>? DelegationToUserDepartments { get; set; }

		public GeneralModulePermission()
		{

		}
		public GeneralModulePermission(bool isView, bool isEdit,bool isAdd, bool isDelete, List<int> delegationFromUser, DelegationToUser delegationToUser, List<Guid>? delegationFromUserDepartments, List<Guid>? delegationToUserDepartments)
		{
			IsView = isView;
			IsAdd = isAdd;
			IsEdit = isEdit;
			IsDelete = isDelete;
			DelegationFromUser = delegationFromUser;
			DelegationToUser = delegationToUser;
			DelegationFromUserDepartments = delegationFromUserDepartments;
			DelegationToUserDepartments = delegationToUserDepartments;
		}
		
		public GeneralModulePermissionDto CopyToDto()
		{
			return new GeneralModulePermissionDto
			{
				Id = Id,
				PrivilegeId = PrivilegeId,
				GeneralModuleId = GeneralModuleId,
				IsView = IsView,
				IsEdit = IsEdit,
				IsAdd = IsAdd,
				IsDelete = IsDelete,
				DelegationFromUser = DelegationFromUser,
				DelegationToUser = DelegationToUser,
				DelegationFromUserDepartments = DelegationFromUserDepartments,	
				DelegationToUserDepartments = DelegationToUserDepartments
			};
		}
	}
}
