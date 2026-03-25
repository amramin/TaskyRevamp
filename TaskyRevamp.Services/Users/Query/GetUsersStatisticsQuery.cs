using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.AuditLog;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Account;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using User = TaskyRevamp.Domain.Models.Users.User;

namespace TaskyRevamp.Services.Users.Query
{
	public record GetUsersStatisticsQuery() : IRequest<UserStatisticsDto>;
	public class GetUsersStatisticsHandler : IRequestHandler<GetUsersStatisticsQuery, UserStatisticsDto>
	{
		private readonly IRepository<User> _userRepository;
        private readonly IRepository<TaskAssignee> _taskassigneerepository;
        private readonly IRepository<AuditLog> _auditlogrepository;
        private readonly IRepository<TaskItem> _taskitemrepository;
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		public GetUsersStatisticsHandler(IRepository<User> userRepository, IRepository<TaskAssignee> taskassigneerepository, IRepository<AuditLog> auditlogrepository
			, IRepository<TaskItem> taskitemrepository)
		{
			_userRepository = userRepository;
			_taskassigneerepository = taskassigneerepository;
			_auditlogrepository = auditlogrepository;
			_taskitemrepository = taskitemrepository;
		}
		public async Task<UserStatisticsDto> Handle(GetUsersStatisticsQuery request, CancellationToken cancellationToken)
		{
			var users = _userRepository.AllAsNoTracking().Result.Value.ToList();
			var logedusers = _auditlogrepository.FindBy(u => u.AuditAction == Dto.Enums.AuditAction.Login)?.Result?.Value?.Count() ?? 0;
			var assigneeusers = _taskassigneerepository.AllAsNoTracking().Result.Value.Select(p => p.UserId).ToList();
            var creatorusers = _taskitemrepository.AllAsNoTracking().Result.Value.Select(p => p.CreatedById).ToList();
            var mergedUsers = assigneeusers
							.Union(creatorusers)
							.ToList();
            var statistics = new UserStatisticsDto
			{
				TotalUsers = users?.Count() ?? 0,
				ActiveUsers = users?.Where(u => u.IsActive == true).Count() ?? 0,
				InActiveUsers = users?.Where(u => u.IsActive == false).Count() ?? 0,
				LinkedWithTasksUsers = mergedUsers?.Count() ?? 0,
				LinkedWithPrivilagesUsers = users?.Where(p => p.PrivilegeId is not null && p.PrivilegeId != Guid.Empty)?.Count() ?? 0,
				LoggedInUsers = logedusers
			};

            return statistics;
		}
	}
}
