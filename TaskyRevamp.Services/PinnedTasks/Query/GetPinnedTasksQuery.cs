
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.PinnedTasks;


namespace PinnedTasksyRevamp.Services.PinnedTaskss.Query;

public record GetPinnedTasksQuery(Guid Id) : IRequest<PinnedTasksDto>;

public class GetPinnedTasksByIdHandler : IRequestHandler<GetPinnedTasksQuery, PinnedTasksDto>
{
    private readonly IRepository<PinnedTasks> _PinnedTasksRepository;

    public GetPinnedTasksByIdHandler(IRepository<PinnedTasks> PinnedTasksRepository)
    {
        _PinnedTasksRepository = PinnedTasksRepository;
    }

    public async Task<PinnedTasksDto> Handle(GetPinnedTasksQuery request, CancellationToken cancellationToken)
    {
        var res = await _PinnedTasksRepository.FindByKey(request.Id);
        PinnedTasksDto PinnedTasksModel = res.Value.CopyToDto();

        return PinnedTasksModel;
    }

 
}