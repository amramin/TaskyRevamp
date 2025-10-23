using MediatR;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Localization.Resources;
namespace userRevamp.Services.userCQRS.Query

{
    public record GetUsersBySearchValueByRoleQuery(string SearchValue, string Culture,string rol) : IRequest<List<UserDto>>;

    public class GetUsersBySearchValueByRoleHandler : IRequestHandler<GetUsersBySearchValueByRoleQuery, List<UserDto>>
    {
        private readonly IRepository<User> _userRepository;
        public GetUsersBySearchValueByRoleHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetUsersBySearchValueByRoleQuery request, CancellationToken cancellationToken)
        {
            var usersDto = new List<UserDto>();

            var users = await _userRepository.FindBy(x =>x.NameEnglish.Contains(request.SearchValue)|| x.NameArabic.Contains(request.SearchValue) || (x.Email == null ? false : x.Email.Contains(request.SearchValue)));
            if (users.IsFailure) throw new Exception(SharedResources.Errordatabase );
            if (users.Value == null) return usersDto;

            foreach (var user in users.Value)
            {
                usersDto.Add(user.CopyToDto());
            }

            return usersDto;
        }
    }
}
