using DocumentFormat.OpenXml.Bibliography;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskyRevamp.Domain.Models.SystemConfiguration;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using Types = TaskyRevamp.Domain.Models.SystemConfiguration.Type;

namespace TaskyRevamp.Services.SystemConfiguration.TypeConfigurarion.Query
{
    public record GetTypeConfigurationViewQuery(int PageNumber, int pageSize, bool IsCompleted = false) : IRequest<PagedResult<TypeDto>>;
    public class GetTypeConfigurationViewHandler : IRequestHandler<GetTypeConfigurationViewQuery, PagedResult<TypeDto>>
    {
        private readonly IRepository<Types> _typeRepository;
        private readonly IRepository<TaskItem> _taskRepository;
        private readonly string currentLanguage;
        public GetTypeConfigurationViewHandler(IRepository<Types> typeRepository,IRepository<TaskItem> taskRepository)
        {
            _typeRepository = typeRepository;
            _taskRepository = taskRepository;
            currentLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }
        public async Task<PagedResult<TypeDto>> Handle(GetTypeConfigurationViewQuery request, CancellationToken cancellationToken)
        {
            var orderBy = GetOrderBy();
            var typeids = new List<Guid?>();
            var notypedto = new TypeDto();
            bool IsNoType = false;
            if (request.IsCompleted)
            {
                typeids = _taskRepository.AllAsNoTracking().Result.Value.Where(t=>t.StatusId== Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C")).Select(p => p.TaskTypeId).Distinct().ToList();
                IsNoType=_taskRepository.AllAsNoTracking().Result.Value.Where(t => t.StatusId == Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C")).Where(p => p.TaskTypeId == null).Any();


            }
            else
            {
                typeids = _taskRepository.AllAsNoTracking().Result.Value.Where(t => t.StatusId != Guid.Parse("C8D504C7-9402-4F91-223C-08DE3318A61C")).Select(p => p.TaskTypeId).Distinct().ToList();
                IsNoType = _taskRepository.AllAsNoTracking().Result.Value.Where(p => p.TaskTypeId == null).Any();
            }
                Expression<Func<Types, bool>> searchExpression = null;
            searchExpression = s => typeids.Contains(s.Id);

            var res = await _typeRepository.GetPagedAsync(
                                request.PageNumber,
                                request.pageSize,
                                request.IsCompleted?null: u => u.IsDeleted == false,
                                searchExpression,
                                orderBy: orderBy,
                                includeProperties: $"{nameof(Types.CreatedBy)},{nameof(Types.UpdatedBy)}");

            var items = res.Items.Select(u => u.CopyToDto()).ToList();
            if (IsNoType)
            {
                if (request.PageNumber == 1)
                {
                    
                    notypedto.NameArabic = "بدون نوع ";
                    notypedto.NameEnglish = "No Type";
                    items.Insert(0,notypedto);
                }
            }
            return new PagedResult<TypeDto>
            {
                Items = items,
                TotalCount = res.TotalCount,
                PageNumber = request.PageNumber,
                PageSize = request.pageSize
            };
        }
        private Func<IQueryable<Types>, IOrderedQueryable<Types>> GetOrderBy()
        {
                    return q => q.OrderBy(u => u.CreateDate);
        }
    }
}
