using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.ChecklistItems.Command;
using TaskyRevamp.Services.ChecklistItems.Query;
using TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Command;
using TaskyRevamp.Services.SystemConfiguration.RecycleBinSettings.Query;

namespace TaskyRevamp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChecklistItemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateChecklistItem")]
        public async Task<ActionResult<Guid>> CreateChecklistItem([FromBody] ChecklistItemDto ChecklistItemDto)
        {
            var res = await _mediator.Send(new CreateChecklistItemCommand(ChecklistItemDto));
            return Ok(res);
        }
        [HttpPost("UpdateChecklistItem")]
        public async Task<IActionResult> UpdateChecklistItem([FromBody] ChecklistItemDto ChecklistItem)
        {
            return Ok(await _mediator.Send(new UpdateChecklistItemCommand(ChecklistItem)));
        }
        [HttpDelete("DeleteChecklistItem/{id}")]
        public async Task<IActionResult> DeleteChecklistItem(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteChecklistItemCommand(id)));
        }
        [HttpGet("GetChecklistItems/{id}")]
        public async Task<IActionResult> GetChecklistItems(Guid id)
        {
            var all = await _mediator.Send(new GetChecklistItemsQuery(id));
            return Ok(all);
        }
    }
}
