
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;


namespace TaskAssigneesyRevamp.Services.TaskAssigneess.Query;

public record GetTaskAssigneesQuery(Guid Id) : IRequest<TaskAssigneesDto>;

public class GetTaskAssigneesByIdHandler : IRequestHandler<GetTaskAssigneesQuery, TaskAssigneesDto>
{
    private readonly IRepository<TaskAssignees> _TaskAssigneesRepository;

    public GetTaskAssigneesByIdHandler(IRepository<TaskAssignees> TaskAssigneesRepository)
    {
        _TaskAssigneesRepository = TaskAssigneesRepository;
    }

    public async Task<TaskAssigneesDto> Handle(GetTaskAssigneesQuery request, CancellationToken cancellationToken)
    {
        var res = await _TaskAssigneesRepository.FindByKey(request.Id);
        TaskAssigneesDto TaskAssigneesModel = res.Value.CopyToDto();

        return TaskAssigneesModel;
    }

 
}