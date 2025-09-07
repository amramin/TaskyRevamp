using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;

namespace PinnedTasksyRevamp.Services.PinnedTaskss.Commands;

public record CreatePinnedTasksCommand(PinnedTasksDto PinnedTasksDto) : IRequest<Guid>;

public class CreatePinnedTasksHandler : IRequestHandler<CreatePinnedTasksCommand, Guid>
{
    private readonly IRepository<PinnedTasks> _PinnedTasksRepository;

    public CreatePinnedTasksHandler(IRepository<PinnedTasks> PinnedTasksRepository) => _PinnedTasksRepository = PinnedTasksRepository;

    public async Task<Guid> Handle(CreatePinnedTasksCommand request, CancellationToken cancellationToken)
    {
        PinnedTasks PinnedTasks=new PinnedTasks();
        PinnedTasks.SetData(request.PinnedTasksDto);
       
        await _PinnedTasksRepository.Insert(PinnedTasks);
        await _PinnedTasksRepository.SaveChangesAsync();
        return PinnedTasks.Id;

    }
}
