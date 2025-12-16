using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAttachment;
using Attachment = TaskyRevamp.Domain.Models.Task.Attachment;

namespace TaskyRevamp.Services.TaskAttachments.Query
{
	public record GetTaskAttachmentsQuery(Guid taskId) : IRequest<List<AttachmentWithNameDto>>;
	public class GetTaskAttachmentsHandler : IRequestHandler<GetTaskAttachmentsQuery, List<AttachmentWithNameDto>>
	{
		private readonly IRepository<Attachment> _attachmentRepository;
		private string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		public GetTaskAttachmentsHandler(IRepository<Attachment> attachmentRepository)
		{
			_attachmentRepository = attachmentRepository;
		}
		public async Task<List<AttachmentWithNameDto>> Handle(GetTaskAttachmentsQuery request, CancellationToken cancellationToken)
		{
			var result = await _attachmentRepository.FindByWithSelector(
			predicate: a => a.TaskAttachment.TaskItemId == request.taskId,
			selector: a => new AttachmentWithNameDto
			{
				AttachmentDto = new AttachmentDto
				{
					Id = a.Id,
					TaskAttachmentId = a.TaskAttachmentId,
					FileName = a.FileName,
					CreatedById = a.CreatedById,
					CreateDate = a.CreateDate
				},
				CreatedByName = currentCulture == "ar" ? a.CreatedBy.NameArabic: a.CreatedBy.NameEnglish,
			});
			return result.OrderByDescending(a => a.AttachmentDto.CreateDate).ToList();
		}
	}
}
