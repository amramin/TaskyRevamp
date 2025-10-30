using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Models.Users;
using UserDelegations;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.UserDelegation;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Dto.UserDelegation;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;


namespace TaskyRevamp.Services.UserDelegations.Query;

public record GetUsersDelegationsQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDelegation> SearchFields, string SearchText) : IRequest<PagedResult<UserDelegationDto>>;

public class GetUsersDelegationsHandler : IRequestHandler<GetUsersDelegationsQuery, PagedResult<UserDelegationDto>>
{
    private readonly IRepository<UserDelegation> _UserDelegationRepository;


    public GetUsersDelegationsHandler(IRepository<UserDelegation> UserDelegationRepository)
    {
        _UserDelegationRepository = UserDelegationRepository;
    }

    public async Task<PagedResult<UserDelegationDto>> Handle(GetUsersDelegationsQuery request, CancellationToken cancellationToken)
    {
        List<UserDelegationDto> allUserDelegations = new List<UserDelegationDto>();



        var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

        Expression<Func<UserDelegation, bool>> searchExpression = null;
        if (request.SearchFields != null && request.SearchFields.Any())
        {
            var predicates = request.SearchFields.Select(x => UserDelegationSearchFielMap.Map[x]).ToList();
            searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
        }

        var res = await _UserDelegationRepository.GetPagedAsync(
                            request.pageNumber,
                            request.pageSize,
                            null,
                            searchExpression,
                            orderBy: orderBy,
                            includeProperties: $"{nameof(UserDelegation.CreatedBy)},{nameof(UserDelegation.UpdatedBy)},{nameof(UserDelegation.FromUser)},{nameof(UserDelegation.Touser)}");

        foreach (var UserDelegation in res.Items)
        {
            UserDelegationDto dep = UserDelegation.CopyToDto();
            dep.CreatedByName = UserDelegation.CreatedBy?.NameEnglish;
            dep.UpdatedByName = UserDelegation.UpdatedBy?.NameEnglish;
            allUserDelegations.Add(dep);

        }
        return new PagedResult<UserDelegationDto>
        {
            Items = allUserDelegations,
            TotalCount = res.TotalCount,
            PageNumber = request.pageNumber,
            PageSize = request.pageSize
        };

    }



    private Func<IQueryable<UserDelegation>, IOrderedQueryable<UserDelegation>> GetOrderBy(string sortByColumn, bool sortAscending)
    {
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        switch (sortByColumn)
        {
            case "CreateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreateDate)
                    : q => q.OrderByDescending(u => u.CreateDate);

            case "UpdateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdateDate)
                    : q => q.OrderByDescending(u => u.UpdateDate);

            case "CreatedBy":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish);

            case "FromDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.FromDate)
                    : q => q.OrderByDescending(u => u.FromDate);

            case "ToDate":

                return sortAscending
                  ? q => q.OrderBy(u => u.ToDate)
                  : q => q.OrderByDescending(u => u.ToDate);

            case "UpdatedBy":

                return sortAscending
                  ? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
                  : q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);
            case "FromUser":
                if (currentCulture == "en")
                {
                    return sortAscending
                        ? q => q.OrderBy(u => u.FromUser!.NameEnglish)
                        : q => q.OrderByDescending(u => u.FromUser!.NameEnglish);
                }
                else
                {
                    return sortAscending
                        ? q => q.OrderBy(u => u.FromUser!.NameArabic)
                        : q => q.OrderByDescending(u => u.FromUser!.NameArabic);


                }
            case "ToUser":
                if (currentCulture == "en")
                {
                    return sortAscending
                        ? q => q.OrderBy(u => u.Touser!.NameEnglish)
                        : q => q.OrderByDescending(u => u.Touser!.NameEnglish);
                }
                else
                {
                    return sortAscending
                        ? q => q.OrderBy(u => u.Touser!.NameArabic)
                        : q => q.OrderByDescending(u => u.Touser!.NameArabic);


                }
            default:
                return q => q.OrderBy(u => u.CreateDate);
        }
    }

}