using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using TaskyRevamp.Dto.TaskSourceDto;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.TaskSources.Query;

public record GetTaskSourceFillterQuery() : IRequest<List<SourceDto>>;

public class GetTaskSourceFillterHandler : IRequestHandler<GetTaskSourceFillterQuery, List<SourceDto>>
{
    private readonly IRepository<Source> _TaskSourceRepository;
    private readonly IRepository<TaskItem> _TaskItemRepository;

    public GetTaskSourceFillterHandler(IRepository<Source> TaskSourceRepository, IRepository<TaskItem> taskItemRepository)
    {
        _TaskSourceRepository = TaskSourceRepository;
        _TaskItemRepository = taskItemRepository;
    }

    public async Task<List<SourceDto>> Handle(GetTaskSourceFillterQuery request, CancellationToken cancellationToken)
    {
        List<SourceDto> allTaskSources = new List<SourceDto>();


        var data = await _TaskSourceRepository.FindBy(K => K.Id != null);



        foreach (var TaskSource in data.Value)
        {
            SourceDto dep = TaskSource.CopyToDto();
            if (!dep.IsActive || dep.IsDeleted)
            {
                var tasks = await _TaskItemRepository.FindBy(t => t.TaskSourceId == dep.Id);
                if (tasks != null && tasks.Value.Any())
                {
                    allTaskSources.Add(dep);
                }
            }
            else
            {
                allTaskSources.Add(dep);
            }

        }

        return allTaskSources;
    }

}