using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskAssigneesyRevamp.Services.TaskAssigneess.Commands;

public record CreateTaskAssigneesCommand(TaskAssigneesDto TaskAssigneesDto) : IRequest<Guid>;

public class CreateTaskAssigneesHandler : IRequestHandler<CreateTaskAssigneesCommand, Guid>
{
    private readonly IRepository<TaskAssignees> _TaskAssigneesRepository;

    public CreateTaskAssigneesHandler(IRepository<TaskAssignees> TaskAssigneesRepository) => _TaskAssigneesRepository = TaskAssigneesRepository;

    public async Task<Guid> Handle(CreateTaskAssigneesCommand request, CancellationToken cancellationToken)
    {
        TaskAssignees TaskAssignees=new TaskAssignees();
        TaskAssignees.SetData(request.TaskAssigneesDto);
       
        await _TaskAssigneesRepository.Insert(TaskAssignees);
        await _TaskAssigneesRepository.SaveChangesAsync();
        return TaskAssignees.Id;

    }
}
