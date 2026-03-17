using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskyRevamp.Dto.TaskChecklist;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.TaskChecklists.Commands;
using TaskyRevamp.Services.TaskChecklists.Query;
using DocumentFormat.OpenXml.Bibliography;

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
    public async Task<IActionResult> CreateTaskChecklist([FromBody] TaskChecklistDto TaskChecklistDto)
    {
		return Ok(await _mediator.Send(new CreateTaskChecklistCommand(TaskChecklistDto)));
    }

    [HttpPost("UpdateTaskChecklist")]
    public async Task<IActionResult> UpdateTaskChecklist([FromBody] TaskChecklistDto TaskChecklist)
    {
        return Ok(await _mediator.Send(new UpdateTaskChecklistCommand(TaskChecklist)));
    }

    [HttpDelete("DeleteCheklist/{taskId}")]
    public async Task<IActionResult> Delete(Guid taskId)
    {
        return Ok(await _mediator.Send(new DeleteTaskChecklistCommand(taskId)));
    }

    [HttpGet("GetAllTaskChecklists/{taskId}")]
    public async Task<IActionResult> GetTaskChecklists(Guid taskId)
    {
        var all = await _mediator.Send(new GetTaskChecklistsQuery(taskId));
        return Ok(all);
    }
}