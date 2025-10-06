using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;

namespace TaskyRevamp.Services.PinnedTasks.Command;

public record CreatePinnedTasksCommand(PinnedTasksDto PinnedTasksDto) : IRequest<Guid>;

public class CreatePinnedTasksHandler : IRequestHandler<CreatePinnedTasksCommand, Guid>
{
    private readonly IRepository<Domain.Models.Task.PinnedTasks> _pinnedTasksRepository;

    public CreatePinnedTasksHandler(IRepository<Domain.Models.Task.PinnedTasks> pinnedTasksRepository) => _pinnedTasksRepository = pinnedTasksRepository;

    public async Task<Guid> Handle(CreatePinnedTasksCommand request, CancellationToken cancellationToken)
    {
        var pinnedTasks=new Domain.Models.Task.PinnedTasks();
        pinnedTasks.SetData(request.PinnedTasksDto);
       
        await _pinnedTasksRepository.Insert(pinnedTasks);
        await _pinnedTasksRepository.SaveChangesAsync();
        return pinnedTasks.Id;

    }
}
