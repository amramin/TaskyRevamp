
using DocumentFormat.OpenXml.Bibliography;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using Priority = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query
{
    public record CheckRelatedComplatedTaskitemPriorityQuery(PriorityDto PriorityDto) : IRequest<bool>;


    public class CheckRelatedComplatedTaskitemPriorityHandler : IRequestHandler<CheckRelatedComplatedTaskitemPriorityQuery, bool>
    {
        private readonly IRepository<TaskItem> _TaskItemRepository;
        private readonly IRepository<Priority> _PriorityRepository;

        public CheckRelatedComplatedTaskitemPriorityHandler(IRepository<TaskItem> TaskItemRepository, IRepository<Priority> PriorityRepository)

        {
            _TaskItemRepository = TaskItemRepository;
            _PriorityRepository = PriorityRepository;

        }


        public async Task<bool> Handle(CheckRelatedComplatedTaskitemPriorityQuery request, CancellationToken cancellationToken)
        {
            var deleted = await _PriorityRepository.FindBy(k => k.NameArabic == request.PriorityDto.NameArabic || k.NameEnglish == request.PriorityDto.NameEnglish && k.IsDeleted);

            var olddata = deleted.Value.FirstOrDefault();
            if (olddata != null)
            {
                var data = await _TaskItemRepository.FindBy(k => k.PriorityId == olddata.Id && k.StatusId == Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C"));

                if (data != null && data.Value.Count > 0)
                {

                    return true;

                }

            }
            return false;

        }
    }
}
