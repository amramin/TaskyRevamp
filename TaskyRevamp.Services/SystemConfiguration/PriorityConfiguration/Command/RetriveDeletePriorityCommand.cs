using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.SystemConfiguration;
using PrioritySetting = TaskyRevamp.Domain.Models.SystemConfiguration.PrioritySettings;

namespace TaskyRevamp.Services.SystemConfiguration.PriorityConfiguration.Command
{
    public record RetriveDeletePriorityCommand(PriorityDto PriorityDto) : IRequest<bool>;
    public class RetriveDeletePriorityHandler : IRequestHandler<RetriveDeletePriorityCommand, bool>
    {
        private readonly IRepository<PrioritySetting> _PriorityRepository;
        private readonly IRepository<TaskItem> _TaskItemRepository;

        public RetriveDeletePriorityHandler(IRepository<PrioritySetting> PriorityRepository, IRepository<TaskItem> TaskItemRepository)
        {
            _PriorityRepository = PriorityRepository;
            _TaskItemRepository = TaskItemRepository;
        }
        public async Task<bool> Handle(RetriveDeletePriorityCommand request, CancellationToken cancellationToken)
        {
            var restored = await _PriorityRepository.FindBy(k => k.Id == request.PriorityDto.Id);// (k.NameArabic == request.PriorityDto.NameArabic || k.NameEnglish == request.PriorityDto.NameEnglish) && (k.Id != request.PriorityDto.Id));
            var res = await _PriorityRepository.FindBy(k => (k.NameArabic == request.PriorityDto.NameArabic || k.NameEnglish == request.PriorityDto.NameEnglish) && (k.Id != request.PriorityDto.Id));
            if (res.Success && res != null && res.Value != null && restored.Success && restored != null && restored.Value != null)
            {
                var PriorityData = res.Value.FirstOrDefault();
                PriorityData.IsDeleted = false;
                var allOldPriorityTasks = await _TaskItemRepository.FindBy(k => k.PriorityId == request.PriorityDto.Id);
                if (allOldPriorityTasks != null && allOldPriorityTasks.Value != null && allOldPriorityTasks.Value.Count > 0)
                {
                    var oldPriorityTasks = allOldPriorityTasks.Value;
                    oldPriorityTasks.ForEach(async task =>
                    {
                        task.PriorityId = PriorityData.Id;
                        await _TaskItemRepository.Update(task);
                    });
                }
                await _PriorityRepository.Update(PriorityData);
                await _PriorityRepository.Delete(request.PriorityDto.Id);
            }
            return true;
        }
    }
}
