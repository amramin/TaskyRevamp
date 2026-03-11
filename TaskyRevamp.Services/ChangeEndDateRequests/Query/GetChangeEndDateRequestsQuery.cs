using DocumentFormat.OpenXml.Bibliography;
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Query;

public record GetChangeEndDateRequestsQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldChangeDueDate> SearchFields, string SearchText) : IRequest<PagedResult<ChangeEndDateRequestDto>>;

public class
    GetChangeEndDateRequestsHandler : IRequestHandler<GetChangeEndDateRequestsQuery, PagedResult<ChangeEndDateRequestDto>>
{
    private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;
    string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;


    public GetChangeEndDateRequestsHandler(IRepository<ChangeEndDateRequest> changeEndDateRequestRepository)
    {
        _changeEndDateRequestRepository = changeEndDateRequestRepository;
    }

    public async Task<PagedResult<ChangeEndDateRequestDto>> Handle(GetChangeEndDateRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var changeEndDateRequestsDto = new List<ChangeEndDateRequestDto>();
        var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);
        Expression<Func<ChangeEndDateRequest, bool>> searchExpression = null;
        if (request.SearchFields != null && request.SearchFields.Any())
        {
           var map = RequestChangeDueDateSearchFieldMap.Map(currentCulture);
            var predicates = request.SearchFields.Select(x => map[x]).ToList();
            searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
        }
        var res = await _changeEndDateRequestRepository.GetPagedAsync(
                    request.pageNumber,
                    request.pageSize,
                        u=>u.Status==ChangeRequestStatus.Pending,
                    searchExpression,
                    orderBy: orderBy,
                    includeProperties: $"{nameof(ChangeEndDateRequest.CreatedBy)},{nameof(ChangeEndDateRequest.Task)}");
        var items = res.Items.Select(u => u.CopyToDto()).ToList();
        return new PagedResult<ChangeEndDateRequestDto>
        {
            Items = items.ToList(),
            TotalCount = res.TotalCount,
            PageNumber = request.pageNumber,
            PageSize = request.pageSize
        };
    }
    private Func<IQueryable<ChangeEndDateRequest>, IOrderedQueryable<ChangeEndDateRequest>> GetOrderBy(string sortByColumn, bool sortAscending)
    {
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        switch (sortByColumn)
        {
            case "Enddate":
                return sortAscending
                    ? q => q.OrderBy(u => u.Task.EndDate)
                    : q => q.OrderByDescending(u => u.Task.EndDate);
            case "NewEndDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.NewEndDate)
                    : q => q.OrderByDescending(u => u.NewEndDate);

            case "Reason":
                return sortAscending
                    ? q => q.OrderBy(u => u.Reason)
                    : q => q.OrderByDescending(u => u.Reason);

            case "Requester":
                if (currentCulture == "ar")
                {
                    return sortAscending
                    ? q => q.OrderBy(u => u.CreatedBy!.NameArabic)
                    : q => q.OrderByDescending(u => u.CreatedBy!.NameArabic);
                }
                else
                {
                    return sortAscending
                    ? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish);
                }

            case "IsActive":
                return sortAscending
                    ? q => q.OrderBy(u => u.Status)
                    : q => q.OrderByDescending(u => u.Status);

            default:
                return q => q.OrderBy(u => u.CreateDate);
        }
    }
}