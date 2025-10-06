using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.PinnedTasks;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.PinnedTasks.Command;
using TaskyRevamp.Services.PinnedTasks.Query;

namespace PinnedTasksRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PinnedTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public PinnedTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreatePinnedTasks")]
    public async Task<ActionResult<string>> CreatePinnedTasks([FromBody] PinnedTasksDto PinnedTasksDto)
    {
        var res = await _mediator.Send(new CreatePinnedTasksCommand(PinnedTasksDto));



        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdatePinnedTasks([FromBody] PinnedTasksDto PinnedTasks)
    {
        return Ok(await _mediator.Send(new UpdatePinnedTasksCommand(PinnedTasks)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeletePinnedTasksCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllPinnedTaskss")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetPinnedTasksQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetPinnedTaskQuery(new Guid(id)));


        return Ok(Task);
    }


}