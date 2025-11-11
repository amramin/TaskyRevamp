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

public record GetTaskTypesQuery() : IRequest<List<TypeDto>>;

public class GetTaskTypesHandler : IRequestHandler<GetTaskTypesQuery, List<TypeDto>>
{
    private readonly IRepository<Types> _TaskTypeRepository;


    public GetTaskTypesHandler(IRepository<Types> TaskTypeRepository)
    {
        _TaskTypeRepository = TaskTypeRepository;
    }

    public async Task<List<TypeDto>> Handle(GetTaskTypesQuery request, CancellationToken cancellationToken)
    {
        List<TypeDto> allTaskTypes = new List<TypeDto>();


        var data = await _TaskTypeRepository.FindBy(K => K.Id != null);



        foreach (var TaskType in data.Value)
        {
            TypeDto dep = TaskType.CopyToDto();

            allTaskTypes.Add(dep);

        }

        return allTaskTypes;
    }

}