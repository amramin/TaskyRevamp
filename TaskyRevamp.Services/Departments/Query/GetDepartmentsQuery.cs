using MediatR;
using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.Department;
using TaskyRevamp.Dto.Enums.SearchFields;
using TaskyRevamp.Dto.GeneralDto;
using TaskyRevamp.Services.Helpers;
using TaskyRevamp.Services.SearchMappings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskyRevamp.Services.Departments.Query;

public record GetDepartmentsQuery(int pageNumber, int pageSize, string sortByColumnName, bool sortAscending, List<SearchFieldDepartment> SearchFields, string SearchText) : IRequest<PagedResult<DepartmentDto>>;

public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, PagedResult<DepartmentDto>>
{
    private readonly IRepository<Department> _departmentRepository;


    public GetDepartmentsHandler(IRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<PagedResult<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDto> allDepartments = new List<DepartmentDto>();



        var orderBy = GetOrderBy(request.sortByColumnName, request.sortAscending);

        Expression<Func<Department, bool>> searchExpression = null;
        if (request.SearchFields != null && request.SearchFields.Any())
        {
            var predicates = request.SearchFields.Select(x => DepartmentSearchFieldDepartmentMap.Map[x]).ToList();
            searchExpression = ExpressionBuilder.BuildLikeExpression(predicates, request.SearchText);
        }

        var res = await _departmentRepository.GetPagedAsync(
                            request.pageNumber,
                            request.pageSize,
                            null,
                            searchExpression,
                            orderBy: orderBy,
                            includeProperties: $"{nameof(Department.CreatedBy)},{nameof(Department.UpdatedBy)},{nameof(Department.Parentdepartment)}");

        //var items = res.Items.Select(u => new DepartmentDto
        //{
        //    Source = u.CopyToDto(),
        ////    CreatedByName = u.CreatedBy != null ? u.CreatedBy.NameEnglish : string.Empty,
        ////    UpdatedByName = u.UpdatedBy != null ? u.UpdatedBy.NameEnglish : string.Empty
        //}).ToList();

      //  res.Items.ForEach(k => allDepartments.Add(k.CopyToDto()));
        foreach (var Department in res.Items)
        {
            DepartmentDto dep = Department.CopyToDto();
            dep.CreatedByName = Department.CreatedBy?.NameEnglish;
            dep.UpdatedByName = Department.UpdatedBy?.NameEnglish;
            allDepartments.Add(dep);

        }
        return new PagedResult<DepartmentDto>
        {
            Items = allDepartments,
            TotalCount = res.TotalCount,
            PageNumber = request.pageNumber,
            PageSize = request.pageSize
        };
    
    }
    private Func<IQueryable<Department>, IOrderedQueryable<Department>> GetOrderBy(string sortByColumn, bool sortAscending)
    {
        string currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        switch (sortByColumn)
        {
            case "CreateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreateDate)
                    : q => q.OrderByDescending(u => u.CreateDate);
            case "NameArabic":
                return sortAscending
                    ? q => q.OrderBy(u => u.NameArabic)
                    : q => q.OrderByDescending(u => u.NameArabic);
            case "NameEnglish":
                return sortAscending
                    ? q => q.OrderBy(u => u.NameEnglish)
                    : q => q.OrderByDescending(u => u.NameEnglish);
            case "UpdateDate":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdateDate)
                    : q => q.OrderByDescending(u => u.UpdateDate);

            case "CreatedBy":
                return sortAscending
                    ? q => q.OrderBy(u => u.CreatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.CreatedBy!.NameEnglish);

            case "UpdatedBy":
                return sortAscending
                    ? q => q.OrderBy(u => u.UpdatedBy!.NameEnglish)
                    : q => q.OrderByDescending(u => u.UpdatedBy!.NameEnglish);

            case "DepartmentParent":
                if (currentCulture == "ar")
                {
                    return sortAscending
                        ? q => q.OrderBy(u => u.Parentdepartment!.NameArabic)
                        : q => q.OrderByDescending(u => u.Parentdepartment!.NameArabic);
                }
                else
                {
                    return sortAscending
                       ? q => q.OrderBy(u => u.Parentdepartment!.NameEnglish)
                        : q => q.OrderByDescending(u => u.Parentdepartment!.NameEnglish);

                }

            default:
                return q => q.OrderBy(u => u.CreateDate);
        }
    }

}