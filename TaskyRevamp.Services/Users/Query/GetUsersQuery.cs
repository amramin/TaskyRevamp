using MediatR;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Localization.Resources;

namespace TaskyRevamp.Services.User.Query
{
    public record GetUsersQuery : IRequest<List<UserDto>>;

    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IRepository<TaskyRevamp.Domain.Models.Users.User> _userRepository;
        public GetUsersHandler(IRepository<TaskyRevamp.Domain.Models.Users.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersDto = new List<UserDto>();
            var users = await _userRepository.All();
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
