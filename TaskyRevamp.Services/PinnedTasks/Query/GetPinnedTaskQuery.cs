using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;

namespace TaskyRevamp.Services.PinnedTasks.Query;

public record GetPinnedTaskQuery(Guid Id) : IRequest<PinnedTasksDto>;

public class GetPinnedTasksByIdHandler : IRequestHandler<GetPinnedTaskQuery, PinnedTasksDto>
{
    private readonly IRepository<Domain.Models.Task.PinnedTasks> _pinnedTasksRepository;

    public GetPinnedTasksByIdHandler(IRepository<Domain.Models.Task.PinnedTasks> pinnedTasksRepository)
    {
        _pinnedTasksRepository = pinnedTasksRepository;
    }

    public async Task<PinnedTasksDto> Handle(GetPinnedTaskQuery request, CancellationToken cancellationToken)
    {
        var res = await _pinnedTasksRepository.FindByKey(request.Id);
        var pinnedTasksModel = res.Value.CopyToDto();

        return pinnedTasksModel;
    }

 
}