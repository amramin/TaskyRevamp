using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;

namespace TaskyRevamp.Services.ChecklistItems.Command;

public record CreateChecklistItemCommand(ChecklistItemDto ChecklistItemDto) : IRequest<Guid>;

public class CreateChecklistItemHandler : IRequestHandler<CreateChecklistItemCommand, Guid>
{
    private readonly IRepository<ChecklistItem> _checklistItemRepository;

    public CreateChecklistItemHandler(IRepository<ChecklistItem> checklistItemRepository)
    {
        _checklistItemRepository = checklistItemRepository;
    }

    public async Task<Guid> Handle(CreateChecklistItemCommand request, CancellationToken cancellationToken)
    {
        var checklistItem = new ChecklistItem(
            request.ChecklistItemDto.Id, request.ChecklistItemDto.TitleEnglish, request.ChecklistItemDto.TitleArabic,
            request.ChecklistItemDto.TaskChecklistId, request.ChecklistItemDto.CreatedById,
            request.ChecklistItemDto.AssignedUserId);


        await _checklistItemRepository.Insert(checklistItem);
        await _checklistItemRepository.SaveChangesAsync();
        return checklistItem.Id;
    }
}