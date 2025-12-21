using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Dto.Enums
{
	public enum Permission
	{
		[LocalizedDescription("CreateTask", typeof(SharedResources))]
		[Order(1)]
		CreateTask,
		[LocalizedDescription("View", typeof(SharedResources))]
		[Order(2)]
		View,
		[LocalizedDescription("Delete", typeof(SharedResources))]
		[Order(3)]
		Delete,
		[LocalizedDescription("SubTask", typeof(SharedResources))]
		[Order(4)]
		SubTask,
		[LocalizedDescription("Reject", typeof(SharedResources))]
		[Order(5)]
		Reject,
		[LocalizedDescription("Edit", typeof(SharedResources))]
		[Order(6)]
		Edit,
		[LocalizedDescription("ChangeEndDate", typeof(SharedResources))]
		[Order(7)]
		ChangeEndDate,
		[LocalizedDescription("RequestChangeEndDate", typeof(SharedResources))]
		[Order(8)]
		RequestChangeEndDate,
		[LocalizedDescription("ApproveAndRejectTheRequestChangeEndDate", typeof(SharedResources))]
		[Order(9)]
		ApproveAndRejectTheRequestChangeEndDate,
		[LocalizedDescription("ChangeProgress", typeof(SharedResources))]
		[Order(10)]
		ChangeProgress,
		[LocalizedDescription("ChecklistAdd", typeof(SharedResources))]
		[Order(11)]
		ChecklistAdd,
		[LocalizedDescription("ChecklistUpdate", typeof(SharedResources))]
		[Order(12)]
		ChecklistUpdate,
		[LocalizedDescription("Complete", typeof(SharedResources))]
		[Order(13)]
		Complete,
		[LocalizedDescription("ReOpen", typeof(SharedResources))]
		[Order(14)]
		ReOpen,
		[LocalizedDescription("Details", typeof(SharedResources))]
		[Order(15)]
		Details,
		[LocalizedDescription("AddAttachment", typeof(SharedResources))]
		[Order(16)]
		AddAttachment,
		[LocalizedDescription("AddComment", typeof(SharedResources))]
		[Order(17)]
		AddComment
	}
}
