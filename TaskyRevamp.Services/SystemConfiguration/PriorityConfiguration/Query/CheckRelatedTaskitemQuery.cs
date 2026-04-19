
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
    public record CheckRelatedTaskitemQuery(Guid Id) : IRequest<bool>;


    public class CheckRelatedTaskitemHandler : IRequestHandler<CheckRelatedTaskitemQuery, bool>
    {
        private readonly IRepository<TaskItem> _TaskItemRepository;

        public CheckRelatedTaskitemHandler(IRepository<TaskItem> TaskItemRepository)

        {
            _TaskItemRepository = TaskItemRepository;

        }


        public async Task<bool> Handle(CheckRelatedTaskitemQuery request, CancellationToken cancellationToken)
        {



            var data = await _TaskItemRepository.FindBy(
        k => k.PriorityId == request.Id && k.StatusId != TaskStatusConstants.Completed);

            if (data != null && data.Value.Count > 0)
            {

                return true;

            }


            return false;

        }
    }
}
