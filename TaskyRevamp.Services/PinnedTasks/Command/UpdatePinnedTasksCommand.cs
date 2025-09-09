using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;



namespace PinnedTasksyRevamp.Services.PinnedTaskss.Commands;

public record UpdatePinnedTasksCommand(PinnedTasksDto PinnedTasks) : IRequest<bool>;

public class UpdatePinnedTasksCommandHandler : IRequestHandler<UpdatePinnedTasksCommand, bool>
{
    private readonly IRepository<PinnedTasks> _PinnedTasksRepository;

    public UpdatePinnedTasksCommandHandler(IRepository<PinnedTasks> PinnedTasksRepository)
    {
        _PinnedTasksRepository = PinnedTasksRepository;
    }

    public async Task<bool> Handle(UpdatePinnedTasksCommand request, CancellationToken cancellationToken)
    {
        var PinnedTasksResponse = await _PinnedTasksRepository.FindByKey(request.PinnedTasks.Id);
        if (!PinnedTasksResponse.Success)
        {
            return false;
        }
        var updated = PinnedTasksResponse.Value;
        updated.CopyToDto();
        await _PinnedTasksRepository.Update(updated);

        return true;
    }
}