using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskAssignees;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.TaskAssignees.Command;
using TaskyRevamp.Services.TaskAssignees.Query;

namespace TaskAssigneesRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskAssigneesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskAssigneesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTaskAssignees")]
    public async Task<ActionResult<string>> CreateTaskAssignees([FromBody] TaskAssigneesDto TaskAssigneesDto)
    {
        var res = await _mediator.Send(new CreateTaskAssigneesCommand(TaskAssigneesDto));



        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateTaskAssignees([FromBody] TaskAssigneesDto TaskAssignees)
    {
        return Ok(await _mediator.Send(new UpdateTaskAssigneesCommand(TaskAssignees)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskAssigneesCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllTaskAssigneess")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetTaskAssigneessQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetTaskAssigneesQuery(new Guid(id)));


        return Ok(Task);
    }


}