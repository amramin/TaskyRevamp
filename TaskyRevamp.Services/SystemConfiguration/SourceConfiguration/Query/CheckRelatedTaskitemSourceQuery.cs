
using MediatR;
using TaskyRevamp.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query
{
    public record CheckRelatedTaskitemSourceQuery(Guid Id) : IRequest<bool>;


    public class CheckRelatedTaskitemSourceHandler : IRequestHandler<CheckRelatedTaskitemSourceQuery, bool>
    {
        private readonly IRepository<TaskItem> _TaskItemRepository;

        public CheckRelatedTaskitemSourceHandler(IRepository<TaskItem> TaskItemRepository)

        {
            _TaskItemRepository = TaskItemRepository;

        }


        public async Task<bool> Handle(CheckRelatedTaskitemSourceQuery request, CancellationToken cancellationToken)
        {



            var data = await _TaskItemRepository.FindBy(
        k => k.TaskSourceId == request.Id && k.StatusId != TaskStatusConstants.Completed);

            if (data != null && data.Value.Count > 0)
            {

                return true;

            }


            return false;

        }
    }
}
