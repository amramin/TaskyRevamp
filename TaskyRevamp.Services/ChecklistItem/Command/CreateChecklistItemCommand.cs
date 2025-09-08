using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;

namespace ChecklistItemyRevamp.Services.ChecklistItems.Commands;

public record CreateChecklistItemCommand(ChecklistItemDto checklistItemDto) : IRequest<Guid>;

public class CreateChecklistItemHandler : IRequestHandler<CreateChecklistItemCommand, Guid>
{
    private readonly IRepository<ChecklistItem> _ChecklistItemRepository;

    public CreateChecklistItemHandler(IRepository<ChecklistItem> ChecklistItemRepository) => _ChecklistItemRepository = ChecklistItemRepository;

    public async Task<Guid> Handle(CreateChecklistItemCommand request, CancellationToken cancellationToken)
    {
        ChecklistItem ChecklistItem = new ChecklistItem(request.checklistItemDto.Id, request.checklistItemDto.TitleEnglish, request.checklistItemDto.TitleArabic,request.checklistItemDto.TaskChecklistId, request.checklistItemDto.CreatedById,request.checklistItemDto.AssignedUserId);


        await _ChecklistItemRepository.Insert(ChecklistItem);
        await _ChecklistItemRepository.SaveChangesAsync();
        return ChecklistItem.Id;

    }
}
