using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChecklistItem;

namespace TaskyRevamp.Services.ChecklistItems.Query;

public record GetChecklistItemsQuery(Guid Id) : IRequest<List<ChecklistItemDto>>;
public class GetChecklistItemByIdHandler : IRequestHandler<GetChecklistItemsQuery, List<ChecklistItemDto>>
{
	private readonly IRepository<ChecklistItem> _checklistItemRepository;
	public GetChecklistItemByIdHandler(IRepository<ChecklistItem> checklistItemRepository)
	{
		_checklistItemRepository = checklistItemRepository;
	}
	public async Task<List<ChecklistItemDto>> Handle(GetChecklistItemsQuery request, CancellationToken cancellationToken)
	{
		var res = await _checklistItemRepository.FindBy(c => c.TaskChecklistId == request.Id);
		var checklistValue = res.Value;
		if (checklistValue == null) 
			return new List<ChecklistItemDto>();
		var checklistItemModel = checklistValue.Select(t => t.CopyToDto()).ToList();
		return checklistItemModel;
	}
}