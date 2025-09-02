using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.GeneralDto;
using TaskChecklistyRevamp.Services.TaskChecklists.Commands;
using TaskyRevamp.Services.TaskChecklists.Commands;
using TaskChecklistyRevamp.Services.TaskChecklists.Query;

namespace TaskChecklistRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskChecklistController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskChecklistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTaskChecklist")]
    public async Task<ActionResult<string>> CreateTaskChecklist([FromBody] TaskChecklistDto TaskChecklistDto)
    {
        var res = await _mediator.Send(new CreateTaskChecklistCommand(TaskChecklistDto));



        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateTaskChecklist([FromBody] TaskChecklistDto TaskChecklist)
    {
        return Ok(await _mediator.Send(new UpdateTaskChecklistCommand(TaskChecklist)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskChecklistCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllTaskChecklists")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetTaskChecklistsQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetTaskChecklistQuery(new Guid(id)));


        return Ok(Task);
    }


}