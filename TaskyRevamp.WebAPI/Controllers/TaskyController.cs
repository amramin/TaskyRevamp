using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Tasks.Commands;

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
    public async Task<ActionResult<CommonApiResponse<string>>> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        var res = await _mediator.Send(new CreateTaskCommand(taskDto));

      

        return Ok(res);
    }
}