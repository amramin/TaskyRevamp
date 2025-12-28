using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Repositeries;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;

namespace TaskyRevamp.Services.TaskAttachments.Command
{
	public record DeleteAttachmentCommand(Guid attachmentId, Guid fileId) : IRequest<bool>;
	public class DeleteAttachmentHnalder : IRequestHandler<DeleteAttachmentCommand, bool>
	{
		private readonly IRepository<Attachment> _attachmentRepository;
		private readonly IFileManagement _fileManagement;

		public DeleteAttachmentHnalder(IRepository<Attachment> attachmentRepository, IFileManagement fileManagement)
		{
			_attachmentRepository = attachmentRepository;
			_fileManagement = fileManagement;
		}

		public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
		{
			var res = await _attachmentRepository.FindBy(a => a.Id == request.attachmentId && a.FileId == request.fileId);
			if (res.Success && res.Value != null)
			{
				var attachment = res.Value.FirstOrDefault();
				await _fileManagement.DeleteFile(request.fileId, attachment!.FileType);
			}
			return true;
		}
	}
}
