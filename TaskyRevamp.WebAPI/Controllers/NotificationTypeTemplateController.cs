using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NotificationTypeTemplateyRevamp.Services.NotificationTypeTemplates.Commands;
using TaskyRevamp.Dto.Notification;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Exceptions;
using TaskyRevamp.Services.Notification.Command;
using TaskyRevamp.Services.Notification.Query;
using TaskyRevamp.Services.SystemConfiguration.StatusConfiguration.Query;
using TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTypeTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationTypeTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetNotificationTypeTemplates")]
        public async Task<IActionResult> NotificationTypeTemplates()
        {
            return Ok(await _mediator.Send(new GetNotificationTypeTemplatesQuery()));
        }

        [HttpGet("GetNotificationTypeTemplateById/{id}")]
        public async Task<IActionResult> GetNotificationTypeTemplateByID(Guid id)
        {
            return Ok(await _mediator.Send(new GetNotificationTypeTemplateByIdQuery(id)));
        }

        [HttpPost("AddNotificationTypeTemplate")]
        public async Task<IActionResult> AddNotificationTypeTemplate([FromBody] NotificationTypeTemplateDto NotificationTypeTemplateDto)
        {
            return Ok(await _mediator.Send(new CreateNotificationTypeTemplateCommand(NotificationTypeTemplateDto)));
        }

        //[HttpPost("UpdateNotificationTypeTemplate")]
        //public async Task<IActionResult> UpdateNotificationTypeTemplate([FromBody] NotificationTypeTemplateDto NotificationTypeTemplateDto)
        //{
        //	return Ok(await _mediator.Send(new UpdateNotificationTypeTemplateCommand(NotificationTypeTemplateDto)));
        //}

        [HttpPost("UpdateNotificationTypeTemplates")]
        public async Task<IActionResult> UpdateNotificationTypeTemplates([FromBody] List<NotificationTypeTemplateDto> notificationTypeTemplateDtos)
        {
            return Ok(await _mediator.Send(new UpdateNotificationTypeTemplatesCommand(notificationTypeTemplateDtos)));
        }

        //[HttpDelete("DeleteNotificationTypeTemplate/{id}")]
        //public async Task<IActionResult> DeleteNotificationTypeTemplate(Guid id)
        //{
        //	return Ok(await _mediator.Send(new DeleteNotificationTypeTemplateCommand(new NotificationTypeTemplateDto { Id = id })));
        //}
    }
}
