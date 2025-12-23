using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskAttachment;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Services.TaskAttachments.Command;
using TaskyRevamp.Services.TaskAttachments.Query;
using TaskyRevamp.Services.TaskComment.Command;

namespace TaskyRevamp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskAttachmentController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TaskAttachmentController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("AddTaskAttachment/{taskItemId}")]
		public async Task<ActionResult<bool>> AddTaskAttachment([FromBody] List<UploadAttachmentDto> attachmentsDto, Guid taskItemId)
		{
			return Ok(await _mediator.Send(new AddTaskAttachmentCommand(attachmentsDto, taskItemId)));
		}

		[HttpGet("GetTaskAttachments/{taskItemId}")]
		public async Task<ActionResult<bool>> GetTaskAttachments(Guid taskItemId)
		{
			return Ok(await _mediator.Send(new GetTaskAttachmentsQuery(taskItemId)));
		}

		[HttpGet("GetAttachmentInfo/{fileId}")]
		public async Task<ActionResult<bool>> GetAttachmentInfo(Guid fileId)
		{
			return Ok(await _mediator.Send(new GetFileInfoQuery(fileId)));
		}

		[HttpDelete("DeleteAttachment/{attachmentId}/{fileId}")]
		public async Task<ActionResult<bool>> DeleteAttachment(Guid attachmentId, Guid fileId)
		{
			return Ok(await _mediator.Send(new DeleteAttachmentCommand(attachmentId, fileId)));
		}
	}
}
