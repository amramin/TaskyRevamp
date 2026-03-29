using TaskyRevamp.Domain.Constants;
﻿
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
using Type = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Query
{
    public record CheckRelatedComplatedTaskitemTypeQuery(TypeDto TypeDto) : IRequest<bool>;


    public class CheckRelatedComplatedTaskitemTypeHandler : IRequestHandler<CheckRelatedComplatedTaskitemTypeQuery, bool>
    {
        private readonly IRepository<TaskItem> _TaskItemRepository;
        private readonly IRepository<Type> _TypeRepository;

        public CheckRelatedComplatedTaskitemTypeHandler(IRepository<TaskItem> TaskItemRepository, IRepository<Type> TypeRepository)

        {
            _TaskItemRepository = TaskItemRepository;
            _TypeRepository = TypeRepository;

        }


        public async Task<bool> Handle(CheckRelatedComplatedTaskitemTypeQuery request, CancellationToken cancellationToken)
        {
            var deleted = await _TypeRepository.FindBy(k => k.NameArabic == request.TypeDto.NameArabic || k.NameEnglish == request.TypeDto.NameEnglish && k.IsDeleted && k.Id != request.TypeDto.Id);

            var olddata = deleted.Value.FirstOrDefault();
            if (olddata != null)
            {
                var data = await _TaskItemRepository.FindBy(k => k.TaskTypeId == olddata.Id && k.StatusId == TaskStatusConstants.Completed);

                if (data != null && data.Value.Count > 0)
                {

                    return true;

                }

            }
            return false;

        }
    }
}
