using MediatR;
using TaskyRevamp.Domain.Constants;
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
                    var assigneeResult = await _taskAssigneeRepository.FindBy(t => t.UserId == request.UserId, $"{nameof(TaskAssignee.TaskItem)}");
                    var IsHasTasksassignee = assigneeResult.Value.Any(t => t.TaskItem.StatusId != TaskStatusConstants.Completed &&
                                        t.TaskItem.StatusId != TaskStatusConstants.Deleted);
                    var creatorResult = await _taskRepository.FindBy(t => t.CreatedById == request.UserId && t.StatusId != TaskStatusConstants.Completed &&
                                        t.StatusId != TaskStatusConstants.Deleted);
                    var IsTaskCreator = creatorResult.Value.Any();
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
