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
        public async Task<ActionResult<string>> CreateChecklistItem([FromBody] ChecklistItemDto ChecklistItemDto)
        {
            var res = await _mediator.Send(new CreateChecklistItemCommand(ChecklistItemDto));



            return Ok(res);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateChecklistItem([FromBody] ChecklistItemDto ChecklistItem)
        {
            return Ok(await _mediator.Send(new UpdateChecklistItemCommand(ChecklistItem)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteChecklistItemCommand(Guid.Parse(id))));
        }
        [HttpPost("GetAllAllTask")]
        public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
        {


            var all = await _mediator.Send(new GetChecklistItemsQuery(query));

            return Ok(all);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {

            var Task = await _mediator.Send(new GetChecklistItemQuery(new Guid(id)));


            return Ok(Task);
        }


    }
}
