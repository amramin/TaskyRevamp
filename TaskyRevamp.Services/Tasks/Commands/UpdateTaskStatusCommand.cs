using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskStatusCommand(Guid TaskId, Domain.Models.Task.TaskStatus Status) : IRequest<Unit>;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<User> _usrRepository;

    public UpdateTaskStatusHandler(ITaskRepository taskRepository, IRepository<User> usrRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _taskRepository = taskRepository;
        _usrRepository = usrRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = Guid.Parse(_httpContextAccessor.GetUserId());

        var task = await _taskRepository.GetTaskById(request.TaskId);
        if (task is null)
            throw new Exception("Task Not Found");
        var user = await _usrRepository.FindByKey(currentUserId);
        if (user?.Value is null)
            throw new Exception("User Not Found");

        //task.UpdateStatus(request.Status, user.Value);
        await _taskRepository.UpdateTask(task);
        return Unit.Value;
    }
}