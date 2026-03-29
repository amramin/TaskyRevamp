using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Constants;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using Sources = TaskyRevamp.Domain.Models.SystemConfiguration.Source;

namespace TaskyRevamp.Services.SystemConfiguration.SourceConfiguration.Query
{
    public record GetSourceConfigurationQueryView(int pageNumber, int pageSize, bool IsCompleted = false) : IRequest<PagedResult<SourceDto>>;
    public class GetSourceConfigurationViewHandler : IRequestHandler<GetSourceConfigurationQueryView, PagedResult<SourceDto>>
    {
        private readonly IRepository<Sources> _sourceRepository;
        private readonly IRepository<TaskItem> _taskRepository;
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        public GetSourceConfigurationViewHandler(IRepository<Sources> sourceRepository, IRepository<TaskItem> taskRepository)
        {
            _sourceRepository = sourceRepository;
            _taskRepository = taskRepository;
        }
        public async Task<PagedResult<SourceDto>> Handle(GetSourceConfigurationQueryView request, CancellationToken cancellationToken)
        {
            var orderBy = GetOrderBy();
            var sourceuids = new List<Guid?>();
            bool IsNoSource = false;
            if (request.IsCompleted)
            {
                var allTasks = await _taskRepository.AllAsNoTracking();
                sourceuids = allTasks.Value.Where(t => t.StatusId == TaskStatusConstants.Completed).Select(p => p.TaskSourceId).Distinct().ToList();
                IsNoSource = allTasks.Value.Where(t => t.StatusId == TaskStatusConstants.Completed).Where(p => p.TaskSourceId==null).Any();

            }
            else
            {
                var allTasks = await _taskRepository.AllAsNoTracking();
                sourceuids = allTasks.Value.Select(p => p.TaskSourceId).Distinct().ToList();
                IsNoSource = allTasks.Value.Where(p => p.TaskSourceId == null).Any();
            }
            Expression<Func<Sources, bool>> searchExpression = null;
            searchExpression = s => sourceuids.Contains(s.Id);
            var res = await _sourceRepository.GetPagedAsync(
                                request.pageNumber,
                                request.pageSize,
                                    u => u.IsDeleted == false,
                                searchExpression,
                                orderBy: orderBy,
                                includeProperties: $"{nameof(Sources.CreatedBy)},{nameof(Sources.UpdatedBy)}");

            var items = res.Items.Select(u => u.CopyToDto()).ToList();
            if (IsNoSource)
            {
                if (request.pageNumber == 1)
                {
                    var nosource = new SourceDto() { NameArabic = "مهام ليس لها مصدر", NameEnglish = "No sorce tasks" };
                    items.Insert(0, nosource);
                }
            }
            return new PagedResult<SourceDto>
            {
                Items = items,
                TotalCount = res.TotalCount,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize
            };
        }

        private Func<IQueryable<Sources>, IOrderedQueryable<Sources>> GetOrderBy()
        {                    
            return q => q.OrderBy(u => u.CreateDate);
        }
    }
}
