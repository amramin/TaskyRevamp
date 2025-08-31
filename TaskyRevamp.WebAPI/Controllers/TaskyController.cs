using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Domain.Exceptions;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Tasks.Commands;
using TaskyRevamp.Services.Tasks.Query;

namespace TaskyRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskyController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTask")]
    public async Task<ActionResult<string>> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        var res = await _mediator.Send(new CreateTaskCommand(taskDto));

      

        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] CreateTaskDto Task)
    {
        return Ok(await _mediator.Send(new UpdateTaskCommand(Task)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllAllTask")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetTaksQuery(query));

        return Ok(all);
    }

 


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetTaskQuery(new Guid(id)));


        return Ok(Task);
    }

 
}