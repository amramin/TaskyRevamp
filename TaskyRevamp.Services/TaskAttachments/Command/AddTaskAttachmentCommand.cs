using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using TaskAttachment = TaskyRevamp.Domain.Models.Task.TaskAttachments;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;

namespace TaskyRevamp.Services.TaskAttachments.Command
{
	public record AddTaskAttachmentCommand(List<AttachmentDto> AttachmentsDto, Guid taskItemId) : IRequest<bool>;
	public class AddTaskAttachmentHandler : IRequestHandler<AddTaskAttachmentCommand, bool>
	{
		private readonly IRepository<TaskAttachment> _taskAttachmentRepository;
		private readonly IRepository<Attachment> _attachmentRepository;

		public AddTaskAttachmentHandler(IRepository<TaskAttachment> taskAttachmentRepository, IRepository<Attachment> attachmentRepository)
		{
			_taskAttachmentRepository = taskAttachmentRepository;
			_attachmentRepository = attachmentRepository;
		}
		public async Task<bool> Handle(AddTaskAttachmentCommand request, CancellationToken cancellationToken)
		{
			var taskAttachmentsResult = await _taskAttachmentRepository.FindBy(t => t.TaskItemId == request.taskItemId);
			TaskAttachment taskAttachments;
			if (!taskAttachmentsResult.Success || taskAttachmentsResult.Value == null || !taskAttachmentsResult.Value.Any())
			{
				taskAttachments = new TaskAttachment
				{
					Id = Guid.NewGuid(),
					TaskItemId = request.taskItemId
				};
				await _taskAttachmentRepository.Insert(taskAttachments);
			}
			else
			{
				taskAttachments = taskAttachmentsResult.Value.FirstOrDefault()!;
			}
			foreach (var dto in request.AttachmentsDto)
			{
				var attachment = new Attachment(Guid.NewGuid(), dto.FileName, dto.Content, taskAttachments.Id);
				await _attachmentRepository.Insert(attachment);
			}
			
			return true;
		}
	}
}
