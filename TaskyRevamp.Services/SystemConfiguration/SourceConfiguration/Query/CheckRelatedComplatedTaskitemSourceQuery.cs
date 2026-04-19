
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
using Source = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query
{
    public record CheckRelatedComplatedTaskitemSourceQuery(SourceDto SourceDto) : IRequest<bool>;


    public class CheckRelatedComplatedTaskitemSourceHandler : IRequestHandler<CheckRelatedComplatedTaskitemSourceQuery, bool>
    {
        private readonly IRepository<TaskItem> _TaskItemRepository;
        private readonly IRepository<Source> _sourceRepository;

        public CheckRelatedComplatedTaskitemSourceHandler(IRepository<TaskItem> TaskItemRepository, IRepository<Source> sourceRepository)

        {
            _TaskItemRepository = TaskItemRepository;
            _sourceRepository = sourceRepository;

        }


        public async Task<bool> Handle(CheckRelatedComplatedTaskitemSourceQuery request, CancellationToken cancellationToken)
        {
            var deleted = await _sourceRepository.FindBy(k => k.NameArabic == request.SourceDto.NameArabic || k.NameEnglish == request.SourceDto.NameEnglish && k.IsDeleted && k.Id != request.SourceDto.Id);

            var olddata = deleted.Value.FirstOrDefault();
            if (olddata != null)
            {
                var data = await _TaskItemRepository.FindBy(k => k.TaskSourceId == olddata.Id && k.StatusId == TaskStatusConstants.Completed);

                if (data != null && data.Value.Count > 0)
                {

                    return true;

                }

            }
            return false;

        }
    }
}
