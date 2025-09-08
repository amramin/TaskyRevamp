using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskEscalation;

namespace TaskEscalationyRevamp.Services.TaskEscalations.Commands;

public record CreateTaskEscalationCommand(TaskEscalationDto TaskEscalationDto) : IRequest<Guid>;

public class CreateTaskEscalationHandler : IRequestHandler<CreateTaskEscalationCommand, Guid>
{
    private readonly IRepository<TaskEscalation> _TaskEscalationRepository;

    public CreateTaskEscalationHandler(IRepository<TaskEscalation> TaskEscalationRepository) => _TaskEscalationRepository = TaskEscalationRepository;

    public async Task<Guid> Handle(CreateTaskEscalationCommand request, CancellationToken cancellationToken)
    {
        TaskEscalation TaskEscalation=new TaskEscalation();
        TaskEscalation.SetData(request.TaskEscalationDto);
       
        await _TaskEscalationRepository.Insert(TaskEscalation);
        await _TaskEscalationRepository.SaveChangesAsync();
        return TaskEscalation.Id;

    }
}
