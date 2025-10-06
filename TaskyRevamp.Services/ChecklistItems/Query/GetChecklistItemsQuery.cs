using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Services.ChecklistItems.Query;

public record GetChecklistItemsQuery(QueryModel? Query) : IRequest<List<ChecklistItemDto>>;

public class GetChecklistItemsHandler : IRequestHandler<GetChecklistItemsQuery, List<ChecklistItemDto>>
{
    private readonly IRepository<Domain.Models.Task.ChecklistItem> _checklistItemRepository;


    public GetChecklistItemsHandler(IRepository<Domain.Models.Task.ChecklistItem> checklistItemRepository)
    {
        _checklistItemRepository = checklistItemRepository;
    }

    public async Task<List<ChecklistItemDto>> Handle(GetChecklistItemsQuery request,
        CancellationToken cancellationToken)
    {
        var checklistItemsDto = new List<ChecklistItemDto>();


        var checklistItems = await _checklistItemRepository.All();


        foreach (var checklistItem in checklistItems?.Value ?? [])
        {
            checklistItemsDto.Add(new ChecklistItemDto()
            {
                Id = checklistItem.Id,

                TitleEnglish = checklistItem.TitleEnglish,
                TitleArabic = checklistItem.TitleArabic,
                AssignedUserId = checklistItem.AssignedUser.Id,
                EndDate = checklistItem.EndDate,
                IsCompleted = checklistItem.IsCompleted,
                TaskChecklistId = checklistItem.TaskChecklist.Id,
            });
        }
        
        return checklistItemsDto;
    }
}