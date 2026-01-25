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
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfiguration.Command
{
    public record RetriveDeleteTypeCommand(TypeDto TypeDto) : IRequest<bool>;
    public class RetriveDeleteTypeHandler : IRequestHandler<RetriveDeleteTypeCommand, bool>
    {
        private readonly IRepository<Types> _TypeRepository;
        private readonly IRepository<TaskItem> _TaskItemRepository;

        public RetriveDeleteTypeHandler(IRepository<Types> TypeRepository, IRepository<TaskItem> TaskItemRepository)
        {
            _TypeRepository = TypeRepository;
            _TaskItemRepository = TaskItemRepository;
        }
        public async Task<bool> Handle(RetriveDeleteTypeCommand request, CancellationToken cancellationToken)
        {
            var restored = await _TypeRepository.FindBy(k => k.Id == request.TypeDto.Id);// (k.NameArabic == request.TypeDto.NameArabic || k.NameEnglish == request.TypeDto.NameEnglish) && (k.Id != request.TypeDto.Id));
            var res = await _TypeRepository.FindBy(k => (k.NameArabic == request.TypeDto.NameArabic || k.NameEnglish == request.TypeDto.NameEnglish) && (k.Id != request.TypeDto.Id));
            if (res.Success && res != null && res.Value != null && restored.Success && restored != null && restored.Value != null)
            {
                var TypeData = res.Value.FirstOrDefault();
                TypeData.IsDeleted = false;
                var allOldTypeTasks = await _TaskItemRepository.FindBy(k => k.TaskTypeId == request.TypeDto.Id);
                if (allOldTypeTasks != null && allOldTypeTasks.Value != null && allOldTypeTasks.Value.Count > 0)
                {
                    var oldTypeTasks = allOldTypeTasks.Value;
                    oldTypeTasks.ForEach(async task =>
                    {
                        task.TaskTypeId = TypeData.Id;
                        await _TaskItemRepository.Update(task);
                    });
                }
                await _TypeRepository.Update(TypeData);
                await _TypeRepository.Delete(request.TypeDto.Id);
            }
            return true;
        }
    }
}
