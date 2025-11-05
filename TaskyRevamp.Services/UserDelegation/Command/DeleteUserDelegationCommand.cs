using MediatR;
using UserDelegations;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.UserDelegations.Command;

public record DeleteUserDelegationCommand(Guid Id) : IRequest<bool>;

public class DeleteUserDelegationHandler : IRequestHandler<DeleteUserDelegationCommand, bool>
{
    private readonly IRepository<UserDelegation> _UserDelegationRepository;

    public DeleteUserDelegationHandler(IRepository<UserDelegation> UserDelegationRepository)
    {
        _UserDelegationRepository = UserDelegationRepository;
    }

    public async Task<bool> Handle(DeleteUserDelegationCommand request, CancellationToken cancellationToken)
    {
       
       
            await _UserDelegationRepository.Delete(request.Id);
       

        return true;
    }
}