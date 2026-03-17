using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.ChecklistItems.Command;

public record DeleteChecklistItemCommand(Guid Id) : IRequest<bool>;
public class DeleteGroupCommandHandler : IRequestHandler<DeleteChecklistItemCommand, bool>
{
	private readonly IRepository<ChecklistItem> _checklistItemRepository;
	public DeleteGroupCommandHandler(IRepository<ChecklistItem> checklistItemRepository)
	{
		_checklistItemRepository = checklistItemRepository;
	}
	public async Task<bool> Handle(DeleteChecklistItemCommand request, CancellationToken cancellationToken)
	{
		await _checklistItemRepository.Delete(request.Id);
		return true;
	}
}