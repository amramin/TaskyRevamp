using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskChecklist;

namespace TaskChecklistyRevamp.Services.TaskChecklists.Commands;

public record CreateTaskChecklistCommand(TaskChecklistDto TaskChecklistDto) : IRequest<Guid>;

public class CreateTaskChecklistHandler : IRequestHandler<CreateTaskChecklistCommand, Guid>
{
    private readonly IRepository<TaskChecklist> _TaskChecklistRepository;

    public CreateTaskChecklistHandler(IRepository<TaskChecklist> TaskChecklistRepository) => _TaskChecklistRepository = TaskChecklistRepository;

    public async Task<Guid> Handle(CreateTaskChecklistCommand request, CancellationToken cancellationToken)
    {
        TaskChecklist TaskChecklist=new TaskChecklist( request.TaskChecklistDto.TaskId,request.TaskChecklistDto.TitleEnglish,request.TaskChecklistDto.TitleArabic);
       
        await _TaskChecklistRepository.Insert(TaskChecklist);
        await _TaskChecklistRepository.SaveChangesAsync();
        return TaskChecklist.Id;

    }
}
