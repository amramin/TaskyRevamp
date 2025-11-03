using MediatR;
using TaskyRevamp.Domain.Models.Users;
using UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.UserDelegation;


namespace TaskyRevamp.Services.UserDelegations.Query;

public record GetUserDelegationsQuery(Guid fromuserid, PagingParameterModel Paging) : IRequest<PaginatedList<UserDelegationDto>>;

public class GetUserDelegationsHandler : IRequestHandler<GetUserDelegationsQuery, PaginatedList<UserDelegationDto>>
{
    private readonly IRepository<UserDelegation> _UserDelegationRepository;
    private readonly IRepository<User> _userRepository;


    public GetUserDelegationsHandler(IRepository<UserDelegation> UserDelegationRepository, IRepository<User> userRepository)
    {
        _UserDelegationRepository = UserDelegationRepository;
        _userRepository = userRepository;
    }

    public async Task<PaginatedList<UserDelegationDto>> Handle(GetUserDelegationsQuery request, CancellationToken cancellationToken)
    {
        List<UserDelegationDto> UserDelegations = new List<UserDelegationDto>();
        List<UserDelegation> all = new List<UserDelegation>();

        //var users = await _userRepository.All();
        if (request.fromuserid != Guid.Empty) {
            var data = await _UserDelegationRepository.FindWithFiltersAsSplitQuery(k=>k.FromUserId==request.fromuserid ||k.ToUserId==request.fromuserid,"",null, request.Paging, x => x.Id,"desc");
            all = data.Value.ToList();
        }
        else
        {
            var data = await _UserDelegationRepository.AllInclude(request.Paging, x => x.Id, "desc");
            all = data.Value.ToList();
        }
      

        foreach (var UserDelegation in all)
        {
            var Fromuser = (await _userRepository.FindByKey(UserDelegation.FromUserId)).Value;
            var Touser = (await _userRepository.FindByKey(UserDelegation.ToUserId)).Value;

            var UserDelegationDto = UserDelegation.CopyToDto();
            UserDelegationDto.FromUserEn = Fromuser.NameEnglish;
            UserDelegationDto.FromUserAr=Fromuser.NameArabic;
            UserDelegationDto.ToUserEn = Touser.NameEnglish;
            UserDelegationDto.ToUserAr=Touser.NameArabic;
            UserDelegations.Add(UserDelegationDto);
        }

        
        return new PaginatedList<UserDelegationDto> { List = UserDelegations, TotalCount = request.Paging?.Total?? 0 };
    }
}