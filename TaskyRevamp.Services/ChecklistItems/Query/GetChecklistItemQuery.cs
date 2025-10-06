using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;

namespace TaskyRevamp.Services.ChecklistItems.Query;

public record GetChecklistItemQuery(Guid Id) : IRequest<ChecklistItemDto>;

public class GetChecklistItemByIdHandler : IRequestHandler<GetChecklistItemQuery, ChecklistItemDto>
{
    private readonly IRepository<Domain.Models.Task.ChecklistItem> _checklistItemRepository;

    public GetChecklistItemByIdHandler(IRepository<Domain.Models.Task.ChecklistItem> checklistItemRepository)
    {
        _checklistItemRepository = checklistItemRepository;
    }

    public async Task<ChecklistItemDto> Handle(GetChecklistItemQuery request, CancellationToken cancellationToken)
    {
        var res = await _checklistItemRepository.FindByKey(request.Id);
        var checklistItemModel = new ChecklistItemDto()
        {
            Id = res.Value.Id,
          TitleArabic= res.Value.TitleArabic,
          TitleEnglish= res.Value.TitleEnglish,
          TaskChecklistId=res.Value.TaskChecklist.Id,
          IsCompleted=res.Value.IsCompleted,
          EndDate = res.Value.EndDate,
          AssignedUserId=res.Value.AssignedUser.Id
        };

        return checklistItemModel;
    }

 
}