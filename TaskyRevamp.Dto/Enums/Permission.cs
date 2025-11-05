using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Enums
{
	public enum Permission
	{
		CreateTask,
		View,
		Delete,
		SubTask,
		Progress,
		Reject,
		Edit,
		ChangeEndDate,
		ApproveAndRejectTheRequestChangeEndDate,
		ChangeProgress,
		ChecklistAdd,
		ChecklistUpdate,
		Complete,
		ReOpen,
		Details,
		AddAttachment,
		AddComment
	}
}
