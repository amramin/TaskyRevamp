using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;

namespace TaskyRevamp.Services.PinnedTasks.Command;

public record UpdatePinnedTasksCommand(PinnedTasksDto PinnedTasks) : IRequest<bool>;

public class UpdatePinnedTasksCommandHandler : IRequestHandler<UpdatePinnedTasksCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.PinnedTasks> _pinnedTasksRepository;

    public UpdatePinnedTasksCommandHandler(IRepository<Domain.Models.Task.PinnedTasks> pinnedTasksRepository)
    {
        _pinnedTasksRepository = pinnedTasksRepository;
    }

    public async Task<bool> Handle(UpdatePinnedTasksCommand request, CancellationToken cancellationToken)
    {
        var pinnedTasksResponse = await _pinnedTasksRepository.FindByKey(request.PinnedTasks.Id);
        if (!pinnedTasksResponse.Success)
        {
            return false;
        }

        if (pinnedTasksResponse?.Value is null)
        {
            throw new Exception("Pinned Task not found");
        }
        var pinnedTask = pinnedTasksResponse.Value;
        pinnedTask.CopyToDto();
        await _pinnedTasksRepository.Update(pinnedTask);

        return true;
    }
}