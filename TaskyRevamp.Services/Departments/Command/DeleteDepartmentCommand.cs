using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.Departments.Command;

public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteDepartmentCommand, bool>
{
    private readonly IRepository<Department> _tskRepository;

    public DeleteGroupCommandHandler(IRepository<Department> tskRepository)
    {
        _tskRepository = tskRepository;
    }

    public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
      
      

            await _tskRepository.Delete(request.Id);

      
        return true;
    }
}