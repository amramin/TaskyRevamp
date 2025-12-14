using Hangfire.Storage.Monitoring;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;

namespace TaskyRevamp.Services.TaskAttachments.Query
{
	public record GetFileInfoQuery(Guid attachmentId): IRequest<byte[]>;
	public class GetFileInfoHandler : IRequestHandler<GetFileInfoQuery, byte[]>
	{
		private readonly IRepository<Attachment> _attachmentRepository;

		public GetFileInfoHandler(IRepository<Attachment> attachmentRepository)
		{
			_attachmentRepository = attachmentRepository;
		}
		public async Task<byte[]> Handle(GetFileInfoQuery request, CancellationToken cancellationToken)
		{
			var result = await _attachmentRepository.FindByKey(request.attachmentId);

			if (!result.Success || result.Value == null)
			{
				throw new KeyNotFoundException("Attachment not found");
			}
			var attachment = result.Value;
			return attachment.Content;
		}
	}
}
