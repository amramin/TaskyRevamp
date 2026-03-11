using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Search;
using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.ChangeEndDateRequest;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.SystemConfiguration;

namespace TaskyRevamp.Services.ChangeEndDateRequests.Query;

public record GetChangeEndDateRequestsQueryByTaskId(Guid id, int pageNumber, int pageSize, string sortByColumnName, bool sortAscending) : IRequest<PagedResult<ChangeEndDateRequestDto>>;

public class
    GetChangeEndDateRequestsByTaskIdHandler : IRequestHandler<GetChangeEndDateRequestsQueryByTaskId, PagedResult<ChangeEndDateRequestDto>>
{
    private readonly IRepository<ChangeEndDateRequest> _changeEndDateRequestRepository;
    private readonly IRepository<User> _userRepository;


    public GetChangeEndDateRequestsByTaskIdHandler(IRepository<ChangeEndDateRequest> changeEndDateRequestRepository, IRepository<User> userRepository)
    {
        _changeEndDateRequestRepository = changeEndDateRequestRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResult<ChangeEndDateRequestDto>> Handle(GetChangeEndDateRequestsQueryByTaskId request,
        CancellationToken cancellationToken)
    {
        var changeEndDateRequestsDto = new List<ChangeEndDateRequestDto>();
        Expression<Func<ChangeEndDateRequest, bool>> searchExpression = null;
        var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);
        var res = await _changeEndDateRequestRepository.GetPagedAsync(
                    request.pageNumber,
                    request.pageSize,
                        u => u.TaskItemId == request.id,
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