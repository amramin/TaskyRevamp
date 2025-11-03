using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using TaskyRevamp.Dto.TaskTypeDto;

namespace TaskyRevamp.Services.TaskTypes.Query;

public record GetTaskTypesQuery() : IRequest<List<TaskTypeDto>>;

public class GetTaskTypesHandler : IRequestHandler<GetTaskTypesQuery, List<TaskTypeDto>>
{
    private readonly IRepository<TaskType> _TaskTypeRepository;


    public GetTaskTypesHandler(IRepository<TaskType> TaskTypeRepository)
    {
        _TaskTypeRepository = TaskTypeRepository;
    }

    public async Task<List<TaskTypeDto>> Handle(GetTaskTypesQuery request, CancellationToken cancellationToken)
    {
        List<TaskTypeDto> allTaskTypes = new List<TaskTypeDto>();


        var data = await _TaskTypeRepository.FindBy(K => K.Id != null);



        foreach (var TaskType in data.Value)
        {
            TaskTypeDto dep = TaskType.CopyToDto();

            allTaskTypes.Add(dep);

        }

        return allTaskTypes;
    }

}