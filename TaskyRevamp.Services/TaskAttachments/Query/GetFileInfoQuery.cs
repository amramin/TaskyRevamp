using Hangfire.Storage.Monitoring;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Repositeries;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;

namespace TaskyRevamp.Services.TaskAttachments.Query
{
	public record GetFileInfoQuery(Guid fileId) : IRequest<byte[]>;
	public class GetFileInfoHandler : IRequestHandler<GetFileInfoQuery, byte[]>
	{
		private readonly IRepository<Attachment> _attachmentRepository;
		private readonly IFileManagement _fileManagement;

		public GetFileInfoHandler(IRepository<Attachment> attachmentRepository, IFileManagement fileManagement)
		{
			_attachmentRepository = attachmentRepository;
			_fileManagement = fileManagement;
		}
		public async Task<byte[]> Handle(GetFileInfoQuery request, CancellationToken cancellationToken)
		{
			var attachment = await _attachmentRepository.FindBy(a => a.FileId == request.fileId);
			if (attachment == null)
				throw new KeyNotFoundException("File not found.");

			var bytes = await _fileManagement.DownloadFileAsBytes(request.fileId);
			return bytes;
		}
	}
}
