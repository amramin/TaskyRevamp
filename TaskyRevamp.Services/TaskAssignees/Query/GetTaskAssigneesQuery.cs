using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.TaskAssignees;

namespace TaskyRevamp.Services.TaskAssignees.Query;

public record GetTaskAssigneesQuery(Guid Id) : IRequest<TaskAssigneesDto>;

public class GetTaskAssigneesByIdHandler : IRequestHandler<GetTaskAssigneesQuery, TaskAssigneesDto>
{
    private readonly IRepository<Domain.Models.Task.TaskAssignee> _taskAssigneesRepository;

    public GetTaskAssigneesByIdHandler(IRepository<Domain.Models.Task.TaskAssignee> taskAssigneesRepository)
    {
        _taskAssigneesRepository = taskAssigneesRepository;
    }

    public async Task<TaskAssigneesDto> Handle(GetTaskAssigneesQuery request, CancellationToken cancellationToken)
    {
        var res = await _taskAssigneesRepository.FindByKey(request.Id);
        var taskAssigneesModel = res.Value.CopyToDto();

        return taskAssigneesModel;
    }

 
}