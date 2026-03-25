using MediatR;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Services.Users.Command
{
    public record SetUserAsMangerCommand(Guid UserId, bool IsManger) : IRequest<bool>;
    public class SetUserAsMangerHandler : IRequestHandler<SetUserAsMangerCommand, bool>
    {
        private readonly IRepository<User> _userRepository;

        public SetUserAsMangerHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(SetUserAsMangerCommand request, CancellationToken cancellationToken)
        {
            var res=await _userRepository.FindByKey(request.UserId);
            var User = res.Value;
            if (User == null)
            {
                return false;
            }
            else{
                User.IsManager = request.IsManger;
                await _userRepository.Update(User);
               await _userRepository.SaveChangesAsync();
            }

            return true;
        }
    }
}
