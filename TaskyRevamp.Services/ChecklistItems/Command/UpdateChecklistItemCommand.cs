using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;

namespace TaskyRevamp.Services.ChecklistItems.Command;

public record UpdateChecklistItemCommand(ChecklistItemDto ChecklistItem) : IRequest<bool>;
public class UpdateChecklistItemCommandHandler : IRequestHandler<UpdateChecklistItemCommand, bool>
{
    private readonly IRepository<Domain.Models.Task.ChecklistItem> _checklistItemRepository;
    public UpdateChecklistItemCommandHandler(IRepository<Domain.Models.Task.ChecklistItem> checklistItemRepository)
    {
        _checklistItemRepository = checklistItemRepository;
    }
    public async Task<bool> Handle(UpdateChecklistItemCommand request, CancellationToken cancellationToken)
    {
        var checklistItemResponse = await _checklistItemRepository.FindByKey(request.ChecklistItem.Id);
        if (!checklistItemResponse.Success)
        {
            return false;
        }
        var updated = checklistItemResponse.Value;
        updated!.SetData(request.ChecklistItem);
        await _checklistItemRepository.Update(updated);
        return true;
    }
}