using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskEscalation;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.TaskEscalation.Command;
using TaskyRevamp.Services.TaskEscalation.Query;

namespace TaskEscalationRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskEscalationController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskEscalationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTaskEscalation")]
    public async Task<ActionResult<string>> CreateTaskEscalation([FromBody] TaskEscalationDto TaskEscalationDto)
    {
        var res = await _mediator.Send(new CreateTaskEscalationCommand(TaskEscalationDto));



        return Ok(res);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateTaskEscalation([FromBody] TaskEscalationDto TaskEscalation)
    {
        return Ok(await _mediator.Send(new UpdateTaskEscalationCommand(TaskEscalation)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskEscalationCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllTaskEscalations")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetTaskEscalationsQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetTaskEscalationQuery(new Guid(id)));


        return Ok(Task);
    }


}