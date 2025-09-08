using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;



namespace ChecklistItemyRevamp.Services.ChecklistItems.Commands;

public record UpdateChecklistItemCommand(ChecklistItemDto ChecklistItem) : IRequest<bool>;

public class UpdateChecklistItemCommandHandler : IRequestHandler<UpdateChecklistItemCommand, bool>
{
    private readonly IRepository<ChecklistItem> _ChecklistItemRepository;

    public UpdateChecklistItemCommandHandler(IRepository<ChecklistItem> ChecklistItemRepository)
    {
        _ChecklistItemRepository = ChecklistItemRepository;
    }

    public async Task<bool> Handle(UpdateChecklistItemCommand request, CancellationToken cancellationToken)
    {
        var ChecklistItemResponse = await _ChecklistItemRepository.FindByKey(request.ChecklistItem.Id);
        if (!ChecklistItemResponse.Success)
        {
            return false;
        }
        var updated = ChecklistItemResponse.Value;
        updated.SetData(request.ChecklistItem);
        await _ChecklistItemRepository.Update(updated);

        return true;
    }
}