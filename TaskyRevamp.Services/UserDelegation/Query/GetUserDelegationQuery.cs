using MediatR;
using UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.UserDelegation;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Services.UserDelegations.Query;

public record GetUserDelegationQuery(Guid Id) : IRequest<UserDelegationDto>;

public class GetUserDelegationByIdHandler : IRequestHandler<GetUserDelegationQuery, UserDelegationDto>
{
    private readonly IRepository<UserDelegation> _UserDelegationRepository;

    public GetUserDelegationByIdHandler(IRepository<UserDelegation> UserDelegationRepository)
    {
        _UserDelegationRepository = UserDelegationRepository;

    }

    public async Task<UserDelegationDto> Handle(GetUserDelegationQuery request, CancellationToken cancellationToken)
    {
        var res = await _UserDelegationRepository.FindBy(x => x.Id == request.Id, includeProperties: $"{nameof(UserDelegation.CreatedBy)},{nameof(UserDelegation.UpdatedBy)},{nameof(UserDelegation.FromUser)},{nameof(UserDelegation.Touser)}");
        if (res.Value is null || !res.Success)
        {
            throw new Exception(SharedResources.Errordatabase);
        }

        var data = res.Value.FirstOrDefault();
        if (data is null)
        {
            throw new Exception(SharedResources.Errordatabase);
        }

        UserDelegationDto UserDelegationModel = new UserDelegationDto();
        UserDelegationModel=data.CopyToDto();

        return UserDelegationModel;
    }
}