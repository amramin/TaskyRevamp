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
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Command
{
    public record RetriveDeleteSourceCommand(SourceDto SourceDto) : IRequest<bool>;
    public class RetriveDeleteSourceHandler : IRequestHandler<RetriveDeleteSourceCommand, bool>
    {
        private readonly IRepository<Sources> _SourceRepository;
        private readonly IRepository<TaskItem> _TaskItemRepository;

        public RetriveDeleteSourceHandler(IRepository<Sources> SourceRepository, IRepository<TaskItem> TaskItemRepository)
        {
            _SourceRepository = SourceRepository;
            _TaskItemRepository = TaskItemRepository;
        }
        public async Task<bool> Handle(RetriveDeleteSourceCommand request, CancellationToken cancellationToken)
        {

            var restored = await _SourceRepository.FindBy(k => k.Id == request.SourceDto.Id);// (k.NameArabic == request.SourceDto.NameArabic || k.NameEnglish == request.SourceDto.NameEnglish) && (k.Id != request.SourceDto.Id));
            var res = await _SourceRepository.FindBy(k => (k.NameArabic == request.SourceDto.NameArabic || k.NameEnglish == request.SourceDto.NameEnglish) && (k.Id != request.SourceDto.Id));
            if (res.Success && res != null && res.Value != null && restored.Success && restored != null && restored.Value != null)
            {
                var SourceData = res.Value.FirstOrDefault();
                SourceData.IsDeleted = false;
                var allOldSourceTasks = await _TaskItemRepository.FindBy(k => k.TaskSourceId == request.SourceDto.Id);
                if (allOldSourceTasks != null && allOldSourceTasks.Value != null && allOldSourceTasks.Value.Count > 0)
                {
                    var oldSourceTasks = allOldSourceTasks.Value;
                    oldSourceTasks.ForEach(async task =>
                    {
                        task.TaskSourceId = SourceData.Id;
                        await _TaskItemRepository.Update(task);
                    });
                }
                await _SourceRepository.Update(SourceData);
                await _SourceRepository.Delete(request.SourceDto.Id);
            }
            return true;
        }
    }

}