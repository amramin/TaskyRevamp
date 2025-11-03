using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using TaskyRevamp.Dto.TaskSourceDto;

namespace TaskyRevamp.Services.TaskSources.Query;

public record GetTaskSourcesQuery() : IRequest<List<TaskSourceDto>>;

public class GetTaskSourcesHandler : IRequestHandler<GetTaskSourcesQuery, List<TaskSourceDto>>
{
    private readonly IRepository<TaskSource> _TaskSourceRepository;


    public GetTaskSourcesHandler(IRepository<TaskSource> TaskSourceRepository)
    {
        _TaskSourceRepository = TaskSourceRepository;
    }

    public async Task<List<TaskSourceDto>> Handle(GetTaskSourcesQuery request, CancellationToken cancellationToken)
    {
        List<TaskSourceDto> allTaskSources = new List<TaskSourceDto>();


        var data = await _TaskSourceRepository.FindBy(K => K.Id != null);



        foreach (var TaskSource in data.Value)
        {
            TaskSourceDto dep = TaskSource.CopyToDto();

            allTaskSources.Add(dep);

        }

        return allTaskSources;
    }

}