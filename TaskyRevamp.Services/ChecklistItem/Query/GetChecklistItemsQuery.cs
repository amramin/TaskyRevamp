
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.GeneralDto;


namespace ChecklistItemyRevamp.Services.ChecklistItems.Query;

public record GetChecklistItemsQuery(QueryModel? Query) : IRequest<List<ChecklistItemDto>>;

public class GetChecklistItemsHandler : IRequestHandler<GetChecklistItemsQuery, List<ChecklistItemDto>>
{
    private readonly IRepository<ChecklistItem> _ChecklistItemRepository;


    public GetChecklistItemsHandler(IRepository<ChecklistItem> ChecklistItemRepository)
    {
        _ChecklistItemRepository = ChecklistItemRepository;
      
    }

    public async Task<List<ChecklistItemDto>> Handle(GetChecklistItemsQuery request, CancellationToken cancellationToken)
    {
        List<ChecklistItemDto> ChecklistItemss = new List<ChecklistItemDto>();
       

        var data = await _ChecklistItemRepository.All();



        foreach (var ChecklistItem in data.Value)
        {
        ;
            ChecklistItemss.Add(new ChecklistItemDto()
            {
                Id = ChecklistItem.Id,
                
                TitleEnglish = ChecklistItem.TitleEnglish,
                TitleArabic= ChecklistItem.TitleArabic,
                AssignedUserId= ChecklistItem.AssignedUser.Id,
                EndDate= ChecklistItem.EndDate,
                IsCompleted= ChecklistItem.IsCompleted,
                TaskChecklistId= ChecklistItem.TaskChecklist.Id,
                
            });
        }

       // return ChecklistItems.ToList();

        return  ChecklistItemss;
    }

   
}