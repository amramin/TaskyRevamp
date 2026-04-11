using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using TaskyRevamp.Dto.TaskTypeDto;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.TaskTypes.Query;

public record GetTaskTypesForFilterQuery() : IRequest<List<TypeDto>>;

public class GetTaskTypesForFilterHandler : IRequestHandler<GetTaskTypesForFilterQuery, List<TypeDto>>
{
    private readonly IRepository<Types> _TaskTypeRepository;
    private readonly IRepository<TaskItem> _TaskItemRepository;

    public GetTaskTypesForFilterHandler(IRepository<Types> TaskTypeRepository, IRepository<TaskItem> taskItemRepository)
    {
        _TaskTypeRepository = TaskTypeRepository;
        _TaskItemRepository = taskItemRepository;
    }

    public async Task<List<TypeDto>> Handle(GetTaskTypesForFilterQuery request, CancellationToken cancellationToken)
    {
        List<TypeDto> allTaskTypes = new List<TypeDto>();


        var data = await _TaskTypeRepository.FindBy(K => K.Id != null);



        foreach (var TaskType in data.Value)
        {
            TypeDto dep = TaskType.CopyToDto();
            if (!dep.IsActive||dep.IsDeleted)
            {
                var tasks = await _TaskItemRepository.FindBy(t => t.TaskTypeId == dep.Id);
                if(tasks != null&&tasks.Value.Any())
                {
                    allTaskTypes.Add(dep);
                }
            }
            else
            {
                allTaskTypes.Add(dep);
            }
        }

        return allTaskTypes;
    }

}