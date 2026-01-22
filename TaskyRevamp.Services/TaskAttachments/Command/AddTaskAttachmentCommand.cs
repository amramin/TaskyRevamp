using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;
using TaskAttachment = TaskyRevamp.Domain.Models.Task.TaskAttachments;

namespace TaskyRevamp.Services.TaskAttachments.Command
{
	public record AddTaskAttachmentCommand(List<UploadAttachmentDto> AttachmentsDto, Guid taskItemId) : IRequest<bool>;
	public class AddTaskAttachmentHandler : IRequestHandler<AddTaskAttachmentCommand, bool>
	{
		private readonly IRepository<TaskAttachment> _taskAttachmentRepository;
		private readonly IRepository<Attachment> _attachmentRepository;
		private readonly IFileManagement _fileManagement;
		private readonly HashSet<string> SupportedAttachmentExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			".pdf", ".docx", ".doc", ".ppt", ".pptx", ".jpeg", ".jpg", ".png",".txt", ".csv", ".json", ".xml"
		};
		public AddTaskAttachmentHandler(IRepository<TaskAttachment> taskAttachmentRepository, IRepository<Attachment> attachmentRepository, IFileManagement fileManagement)
		{
			_taskAttachmentRepository = taskAttachmentRepository;
			_attachmentRepository = attachmentRepository;
			_fileManagement = fileManagement;
		}
		public async Task<bool> Handle(AddTaskAttachmentCommand request, CancellationToken cancellationToken)
		{
			var taskAttachmentsResult = await _taskAttachmentRepository.FindBy(t => t.TaskItemId == request.taskItemId);
			TaskAttachment taskAttachments;
			if (!taskAttachmentsResult.Success || taskAttachmentsResult.Value == null || !taskAttachmentsResult.Value.Any())
			{
				taskAttachments = new TaskAttachment { Id = Guid.NewGuid(), TaskItemId = request.taskItemId };
				await _taskAttachmentRepository.Insert(taskAttachments);
			}
			else
				taskAttachments = taskAttachmentsResult.Value.FirstOrDefault()!;

			foreach (var dto in request.AttachmentsDto)
			{
				var extension = Path.GetExtension(dto.FileName)?.ToLower();
				if (!SupportedAttachmentExtensions.Contains(extension!))
					continue;
				var fileType = _fileManagement.ResolveFileType(dto.FileName);
				var fileId = await _fileManagement.UploadFile(dto.Bytes, dto.FileName, fileType);
				var attachment = new Attachment(Guid.NewGuid(), dto.FileName, fileId, dto.Size, taskAttachments.Id, fileType);
				await _attachmentRepository.Insert(attachment);
			}
			return true;
		}
	}
}
