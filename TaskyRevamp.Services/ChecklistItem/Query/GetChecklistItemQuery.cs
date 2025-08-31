
using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;


namespace ChecklistItemyRevamp.Services.ChecklistItems.Query;

public record GetChecklistItemQuery(Guid Id) : IRequest<ChecklistItemDto>;

public class GetChecklistItemByIdHandler : IRequestHandler<GetChecklistItemQuery, ChecklistItemDto>
{
    private readonly IRepository<ChecklistItem> _ChecklistItemRepository;

    public GetChecklistItemByIdHandler(IRepository<ChecklistItem> ChecklistItemRepository)
    {
        _ChecklistItemRepository = ChecklistItemRepository;
    }

    public async Task<ChecklistItemDto> Handle(GetChecklistItemQuery request, CancellationToken cancellationToken)
    {
        var res = await _ChecklistItemRepository.FindByKey(request.Id);
        ChecklistItemDto ChecklistItemModel = new ChecklistItemDto()
        {
            Id = res.Value.Id,
          TitleArabic= res.Value.TitleArabic,
          TitleEnglish= res.Value.TitleEnglish,
          TaskChecklistId=res.Value.TaskChecklist.Id,
          IsCompleted=res.Value.IsCompleted,
          EndDate = res.Value.EndDate,
          AssignedUserId=res.Value.AssignedUser.Id
        };

        return ChecklistItemModel;
    }

 
}