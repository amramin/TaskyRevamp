
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query
{
    public record CheckRelatedTaskitemTypeQuery(Guid Id) : IRequest<bool>;


    public class CheckRelatedTaskitemTypeHandler : IRequestHandler<CheckRelatedTaskitemTypeQuery, bool>
    {
        private readonly IRepository<TaskItem> _TaskItemRepository;

        public CheckRelatedTaskitemTypeHandler(IRepository<TaskItem> TaskItemRepository)

        {
            _TaskItemRepository = TaskItemRepository;

        }


        public async Task<bool> Handle(CheckRelatedTaskitemTypeQuery request, CancellationToken cancellationToken)
        {



            var data = await _TaskItemRepository.FindBy(
        k => k.TaskTypeId == request.Id && k.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C"));

            if (data != null && data.Value.Count > 0)
            {

                return true;

            }


            return false;

        }
    }
}
