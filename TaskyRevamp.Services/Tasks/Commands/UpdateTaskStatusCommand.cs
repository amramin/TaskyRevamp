using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Tasks.Commands;

public record UpdateTaskStatusCommand(Guid TaskId, Domain.Models.Task.TaskStatus Status) : IRequest<Unit>;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand,Unit>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<User> _usrRepository;

    public UpdateTaskStatusHandler(IRepository<TaskItem> taskRepository, IRepository<User> usrRepository, IHttpContextAccessor httpContextAccessor)
    {
        _taskRepository = taskRepository;
        _usrRepository= usrRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        Guid currentUserId = Guid.Parse(_httpContextAccessor.GetUserId());

        var task = await _taskRepository.FindByKey(request.TaskId);
        var usr = await _usrRepository.FindByKey(currentUserId);

        var res = task.Value;
        res?.UpdateStatus(request.Status,usr.Value);
        await _taskRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
