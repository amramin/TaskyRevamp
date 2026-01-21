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

public record GetTaskSourcesQuery() : IRequest<List<SourceDto>>;

public class GetTaskSourcesHandler : IRequestHandler<GetTaskSourcesQuery, List<SourceDto>>
{
    private readonly IRepository<Source> _TaskSourceRepository;


    public GetTaskSourcesHandler(IRepository<Source> TaskSourceRepository)
    {
        _TaskSourceRepository = TaskSourceRepository;
    }

    public async Task<List<SourceDto>> Handle(GetTaskSourcesQuery request, CancellationToken cancellationToken)
    {
        List<SourceDto> allTaskSources = new List<SourceDto>();


        var data = await _TaskSourceRepository.FindBy(K => K.Id != null);



        foreach (var TaskSource in data.Value)
        {
            SourceDto dep = TaskSource.CopyToDto();
     
            allTaskSources.Add(dep);

        }

        return allTaskSources;
    }

}