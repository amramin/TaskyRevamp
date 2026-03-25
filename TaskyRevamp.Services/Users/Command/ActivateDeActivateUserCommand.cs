using MediatR;
using TaskyRevamp.Domain.Models.Permissions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Permissions;

namespace TaskyRevamp.Services.Users.Command
{
    public record ActivateDeActivateUserCommand(Guid UserId, bool IsActive) : IRequest<bool>;
    public class ActivateDeActivateUserHandler : IRequestHandler<ActivateDeActivateUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<TaskAssignee> _taskAssigneeRepository;
        private readonly IRepository<TaskItem> _taskRepository;

        public ActivateDeActivateUserHandler(IRepository<User> userRepository, IRepository<TaskAssignee> taskAssigneeRepository, IRepository<TaskItem> taskRepository)
        {
            _userRepository = userRepository;
            _taskAssigneeRepository = taskAssigneeRepository;
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(ActivateDeActivateUserCommand request, CancellationToken cancellationToken)
        {
            var res = await _userRepository.FindByKey(request.UserId);
            var User = res.Value;
            if (User == null)
            {
                return false;
            }
            else
            {
                if (request.IsActive == false)
                {
                    var IsHasTasksassignee = _taskAssigneeRepository.FindBy(t => t.UserId == request.UserId, $"{nameof(TaskAssignee.TaskItem)}").Result.Value.Any(t => t.TaskItem.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C") &&
                                        t.TaskItem.StatusId != Guid.Parse("D8E94CCE-586A-46D3-223D-08DE3318A61C"));
                    var IsTaskCreator = _taskRepository.FindBy(t => t.CreatedById == request.UserId && t.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C") &&
                                        t.StatusId != Guid.Parse("D8E94CCE-586A-46D3-223D-08DE3318A61C")).Result.Value.Any();
                    if (IsHasTasksassignee||IsTaskCreator)
                    {
                        return false;
                    }
                }
                User.IsActive = request.IsActive;
                await _userRepository.Update(User);
                await _userRepository.SaveChangesAsync();
            }

            return true;
        }
    }
}
