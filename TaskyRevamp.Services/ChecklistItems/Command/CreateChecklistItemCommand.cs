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
        var checklistItem = new ChecklistItem(request.ChecklistItemDto.Title, request.ChecklistItemDto.TaskChecklistId,
            request.ChecklistItemDto.AssignedUserId, request.ChecklistItemDto.EndDate, request.ChecklistItemDto.IsDone);
        await _checklistItemRepository.Insert(checklistItem);
        return checklistItem.Id;
    }
}