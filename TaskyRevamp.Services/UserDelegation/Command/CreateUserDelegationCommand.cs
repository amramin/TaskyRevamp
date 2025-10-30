using MediatR;
using TaskyRevamp.Domain;
using TaskyRevamp.Domain.Interfaces;
using TaskyRevamp.Domain.Interfaces.Repositeries;
using UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.UserDelegation;


namespace TaskyRevamp.Services.UserDelegations.Command;
public record CreateUserDelegationCommand(UserDelegationDto UserDelegation) : IRequest<bool>;

public class CreateUserDelegationCommandHandler : IRequestHandler<CreateUserDelegationCommand, bool>
{
    private readonly IRepository<UserDelegation> _UserDelegationRepository;

    public CreateUserDelegationCommandHandler(IRepository<UserDelegation> UserDelegationRepository)
    {
        _UserDelegationRepository = UserDelegationRepository;
    }

    public async Task<bool> Handle(CreateUserDelegationCommand request, CancellationToken cancellationToken)
    {

        var all = await _UserDelegationRepository.FindBy(k => k.FromUserId == request.UserDelegation.FromUserId && k.ToUserId == request.UserDelegation.ToUserId && k.FromDate == request.UserDelegation.FromDate && k.ToDate == request.UserDelegation.ToDate);
        if (all != null && all.Value.Count > 0)
        {
            return false;
        }
        var newUserDelegation = new UserDelegation()
        {
            ToDate = request.UserDelegation.ToDate,
            FromDate = request.UserDelegation.FromDate,
            FromUserId = request.UserDelegation.FromUserId,
            ToUserId = request.UserDelegation.ToUserId,
        };

        await _UserDelegationRepository.Insert(newUserDelegation);
        return true;
    }
}