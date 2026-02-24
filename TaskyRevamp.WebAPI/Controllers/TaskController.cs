using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TaskyRevamp.Dto.Enums;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Dto.TaskComment;
using TaskyRevamp.Dto.TaskDto;
using TaskyRevamp.Services.Departments.Query;
using TaskyRevamp.Services.Tasks.Commands;
using TaskyRevamp.Services.Tasks.Query;

namespace TaskyRevamp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto taskDto)
    {
        var res = await _mediator.Send(new CreateTaskCommand(taskDto));
        return Ok(res);
    }

    [HttpGet("CompleteTask/{id}")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        return Ok(await _mediator.Send(new CompleteTaskCommand(id)));
    }

    [HttpPost("ReopenTask")]
    public async Task<IActionResult> ReopenTask([FromBody] TaskCommentDto TaskCommentDto)
    {
        return Ok(await _mediator.Send(new ReopenTaskCommand(TaskCommentDto)));
    }
    [HttpPost("RejectTask")]
    public async Task<IActionResult> RejectTask([FromBody] TaskCommentDto TaskCommentDto)
    {
        return Ok(await _mediator.Send(new RejectTaskCommand(TaskCommentDto)));
    }
    [HttpPost("UpdateTask")]
    public async Task<IActionResult> UpdateTask([FromBody] CreateTaskDto Task)
    {
        return Ok(await _mediator.Send(new UpdateTaskCommand(Task)));
    }

    [HttpGet("UpdateTasksDepartment/{oldId}/{newId}")]
    public async Task<IActionResult> UpdateTasksDepartment(Guid oldId, Guid newId)
    {
        var res = await _mediator.Send(new UpdateTasksDepartmentCommand(oldId, newId));
        return Ok(res);
    }

    [HttpGet("ChangeTaskProgress/{taskId}/{progress}")]
    public async Task<IActionResult> ChangeTaskProgress(Guid taskId, int progress)
    {
        return Ok(await _mediator.Send(new ChangeTaskProgressCommand(taskId, progress)));
    }
    [HttpGet("UpdateTaskPriority/{taskId}/{PriorityId}")]
    public async Task<IActionResult> UpdateTaskPriority(Guid taskId, Guid PriorityId)
    {
        return Ok(await _mediator.Send(new UpdateTaskPriorityCommand(taskId, PriorityId)));
    }
    [HttpGet("CheckOpenedTaskForUser/{userId}")]
    public async Task<IActionResult> CheckOpenedTaskForUser(Guid userId)
    {
        return Ok(await _mediator.Send(new CheckOpenedTaskForUserCommand(userId)));
    }
    [HttpGet("CheckDelayedTasks")]
    public async Task<IActionResult> CheckDelayedTasks()
    {
        return Ok(await _mediator.Send(new CheckDelayedTasksCommand()));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _mediator.Send(new DeleteTaskCommand(Guid.Parse(id))));
    }

    [HttpPost("GetAllTask")]
    public async Task<IActionResult> GetAllTask(
         [FromServices] IOptions<PaginationSettings> paginationSettings,
         [FromQuery] int pageNumber = 1,
         [FromQuery] int? pageSize = null,
         [FromQuery] string sortByColumnName = "CreateDate",
         [FromQuery] bool sortAscending = true,
         [FromQuery] List<SearchFieldTask> searchFields = null,
         [FromQuery] string searchText = null,
         [FromQuery] int viewType = (int)ViewTypes.OverAllView,
        [FromQuery] Guid? viewTypeId = null,
        [FromQuery] bool IsCompleted = false,
        [FromBody] TaskFilterComponent taskFilter=null)
    {

        var size = pageSize ?? paginationSettings.Value.DefaultPageSize;
        var all = await _mediator.Send(new GetTasksQuery(pageNumber, size, sortByColumnName, sortAscending, searchFields, searchText,viewType,viewTypeId,IsCompleted,taskFilter));

        return Ok(all);
    }

    [HttpGet("GetTaskById/{id}/{currentUserId}")]
    public async Task<IActionResult> GetTaskById(Guid id, Guid currentUserId)
    {
        return Ok(await _mediator.Send(new GetTaskQuery(id, currentUserId)));
    }
    [HttpGet("GetTaskById/{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        return Ok(await _mediator.Send(new GetTaskByIdQuery(id)));
    }

    [HttpGet("GetTasksForDDL/{id}")]
    public async Task<IActionResult> GetTasksForDDL(Guid id)
    {
        var all = await _mediator.Send(new GetTasksForDDLQuery(id));
        return Ok(all);
    }

    [HttpGet("GetMainAndParentTasks")]
    public async Task<IActionResult> GetMainAndParentTasks()
    {
        return Ok(await _mediator.Send(new GetMainAndParentTasksQuery()));
    }


}