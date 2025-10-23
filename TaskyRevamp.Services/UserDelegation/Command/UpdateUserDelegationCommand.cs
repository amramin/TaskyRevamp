using MediatR;
using TaskyRevamp.Domain.Models.Users.UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.UserDelegation;

namespace TaskyRevamp.Services.UserDelegations.Command;

public record UpdateUserDelegationCommand(UserDelegationDto UserDelegation) : IRequest<bool>;

public class UpdateUserDelegationHandler : IRequestHandler<UpdateUserDelegationCommand, bool>
{
    private readonly IRepository<UserDelegation> _UserDelegationRepository;

    public UpdateUserDelegationHandler(IRepository<UserDelegation> UserDelegationRepository)
    {
        _UserDelegationRepository = UserDelegationRepository;
    }

    public async Task<bool> Handle(UpdateUserDelegationCommand request, CancellationToken cancellationToken)
    {


        var oldDelegation = await _UserDelegationRepository.FindBy(k => k.Id == request.UserDelegation.Id);
        var newUserDelegation = oldDelegation.Value.FirstOrDefault();
        newUserDelegation.FromDate = request.UserDelegation.FromDate;
        newUserDelegation.ToDate = request.UserDelegation.ToDate;
        newUserDelegation.FromUserId = request.UserDelegation.FromUserId;
        newUserDelegation.ToUserId = request.UserDelegation.ToUserId;
         await _UserDelegationRepository.Update(newUserDelegation);
        return true;
    }
}