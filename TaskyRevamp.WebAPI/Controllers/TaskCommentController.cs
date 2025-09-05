using TaskCommentyRevamp.Services.TaskComments.Commands;
using TaskCommentyRevamp.Services.TaskComments.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.TaskComments.Commands;

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


    [HttpPut]
    public async Task<IActionResult> UpdateTaskComment([FromBody] TaskCommentDto TaskComment)
    {
        return Ok(await _mediator.Send(new UpdateTaskCommentCommand(TaskComment)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskCommentCommand(Guid.Parse(id))));
    }
    [HttpPost("GetAllTaskComments")]
    public async Task<IActionResult> AllTask([FromBody] QueryModel? query = null)
    {


        var all = await _mediator.Send(new GetTaskCommentsQuery(query));

        return Ok(all);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {

        var Task = await _mediator.Send(new GetTaskCommentQuery(new Guid(id)));


        return Ok(Task);
    }


}