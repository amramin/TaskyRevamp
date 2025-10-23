using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Dto.GeneralDto;
namespace TaskyRevamp.Services.User.Query
{   
    public record GetUsersBySearchValueQuery(string SearchValue, 
      
        string Culture, 
        int pageSize, 
        int offset) : IRequest<SearchableBackendDto<DdlDto>>;

    public class GetUsersBySearchValueHandler : IRequestHandler<GetUsersBySearchValueQuery, SearchableBackendDto<DdlDto>>
    {
        private readonly IRepository<TaskyRevamp.Domain.Models.Users.User> _userRepository;
        public GetUsersBySearchValueHandler(IRepository<TaskyRevamp.Domain.Models.Users.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SearchableBackendDto<DdlDto>> Handle(GetUsersBySearchValueQuery request, CancellationToken cancellationToken)
        {
           
            var predicate = PredicateBuilder.True<TaskyRevamp.Domain.Models.Users.User>();
            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                if (request.Culture.Equals("ar"))
                {
                    predicate = predicate.And(x => x.NameArabic.Contains(request.SearchValue));
                }
                else
                {
                    predicate = predicate.And(x => x.NameEnglish.Contains(request.SearchValue));
                }
                predicate = predicate.Or(x => x.Email == null ? false : x.Email.Contains(request.SearchValue));
            }
            
            //if (!string.IsNullOrWhiteSpace(request.role))
            //{
            //    predicate = predicate.And(x => x.Roles.Any(r => r.NameEnglish == request.role));
            //}
            var usersDbRet = await _userRepository.FindByWithSelectorPaginated(predicate, x => new DdlDto {
                    Id = x.Id,
                    NameAr = x.NameArabic,
                    NameEn = x.NameEnglish
                },
                request.pageSize,
                request.offset);
            return new SearchableBackendDto<DdlDto>
            {
                Items = usersDbRet.Data,
                TotalCount = usersDbRet.Count
            };
        }
    }
}
