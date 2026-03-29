
using DocumentFormat.OpenXml.Bibliography;
using MediatR;
using TaskyRevamp.Domain.Constants;
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
            var deleted = await _PriorityRepository.FindBy(k => k.NameArabic == request.PriorityDto.NameArabic || k.NameEnglish == request.PriorityDto.NameEnglish && k.IsDeleted && k.Id != request.PriorityDto.Id);

            var olddata = deleted.Value.FirstOrDefault();
            if (olddata != null)
            {
                var data = await _TaskItemRepository.FindBy(k => k.PriorityId == olddata.Id && k.StatusId == TaskStatusConstants.Completed);

                if (data != null && data.Value.Count > 0)
                {

                    return true;

                }

            }
            return false;

        }
    }
}
