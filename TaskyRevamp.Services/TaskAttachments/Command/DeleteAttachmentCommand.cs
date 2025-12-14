using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;

namespace TaskyRevamp.Services.TaskAttachments.Command
{
	public record DeleteAttachmentCommand(Guid attachmentId) : IRequest<bool>;
	public class DeleteAttachmentHnalder : IRequestHandler<DeleteAttachmentCommand, bool>
	{
		private readonly IRepository<Attachment> _attachmentRepository;

		public DeleteAttachmentHnalder(IRepository<Attachment> attachmentRepository)
		{
			_attachmentRepository = attachmentRepository;
		}

		public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
		{
			var res = await _attachmentRepository.FindByKey(request.attachmentId);
			if(res.Success && res.Value != null)
			{
				await _attachmentRepository.Delete(request.attachmentId);
			}
			return true;
		}
	}
}
