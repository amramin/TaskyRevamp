using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.TaskComment.Command;
using TaskyRevamp.Services.TaskComment.Query;

namespace TaskCommentRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskCommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskCommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTaskComment")]
    public async Task<ActionResult<string>> CreateTaskComment([FromBody] TaskCommentDto TaskCommentDto)
    {
        var res = await _mediator.Send(new CreateTaskCommentCommand(TaskCommentDto));
        return Ok(res);
    }

    [HttpPost("UpdateTaskComment")]
    public async Task<IActionResult> UpdateTaskComment([FromBody] TaskCommentDto TaskComment)
    {
        return Ok(await _mediator.Send(new UpdateTaskCommentCommand(TaskComment)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskCommentCommand(Guid.Parse(id))));
    }

    [HttpGet("GetAllTaskComments/{Id}")]
    public async Task<IActionResult> AllTask(Guid Id)
    {
        return Ok(await _mediator.Send(new GetTaskCommentsQuery(Id)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var Task = await _mediator.Send(new GetTaskCommentQuery(new Guid(id)));
        return Ok(Task);
    }


}