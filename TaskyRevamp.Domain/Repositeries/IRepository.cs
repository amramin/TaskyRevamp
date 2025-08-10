using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Domain.Repositeries;

public interface IRepository<TEntity> where TEntity : Entity

{
    public bool GetDeleted { get; set; }
    public bool GetInActive { get; set; }
    public bool IgnoreAutoIncludes { get; set; }

    Task<DbResponse<IEnumerable<TEntity>>> All();
    Task<DbResponse<IEnumerable<TEntity>>> AllAsNoTracking();
    Task<DbResponse<IEnumerable<TEntity>>> AllAsNoTracking(string includeProperties);
    Task<object> Max(Expression<Func<TEntity, object>> expression, object defaultValue);

    public Task<DbResponse<IEnumerable<TEntity>>> GetInclude(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    Task<DbResponse<IEnumerable<TEntity>>> AllInclude
    (PagingParameterModel? paging = null, Expression<Func<TEntity, object>>? orderBy = null,
        string orderDir = "asc",
        Expression<Func<TEntity, bool>> predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<DbResponse<IEnumerable<TEntity>>> AllIncludeDescending
    (int page = -1, int quantity = 10, string orderBy = "",
        params Expression<Func<TEntity, object>>[] includeProperties);


    Task<DbResponse<IEnumerable<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate,
        PagingParameterModel paging);

    Task<DbResponse<List<TEntity>>> FindWithFilters(Expression<Func<TEntity, bool>> predicate,
        string includeProperties,
        Expression<Func<TEntity, bool>>? filter = null,
        PagingParameterModel? paging = null,
        Expression<Func<TEntity, object>>? orderBy = null);

    Task<DbResponse<List<TEntity>>> FindWithFiltersAsSplitQuery(Expression<Func<TEntity, bool>> predicate,
        string includeProperties,
        Expression<Func<TEntity, bool>>? filter = null,
        PagingParameterModel? paging = null,
        Expression<Func<TEntity, object>>? orderBy = null
        , string orderDir = "asc");

    Task<DbResponse<List<TResult>>> FindWithSelectorFiltersAsSplitQuery<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        string includeProperties,
        Expression<Func<TEntity, bool>>? filter = null,
        PagingParameterModel? paging = null,
        Expression<Func<TEntity, object>>? orderBy = null
        , string orderDir = "asc");

    Task<DbResponse<TEntity?>> FindByKey(Guid id);
    Task<DbResponse<TEntity>> FindByKey(Guid id, string includeProperty);
    Task<DbResponse<TEntity>> FindByKey(Guid id, Expression<Func<TEntity, object>> predicate);

    Task<DbResponse<List<TResult>>> FindByWithSelector<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        string? includeProperties = null);

    Task Insert(TEntity entity);

    Task InsertRange(IEnumerable<TEntity> entities);

    // Task Update(TEntity updatedEntity, TEntity currentEntity);
    //  Task Update();

    Task Update(TEntity updatedEntity);
    void UpdateNoResult(TEntity updatedEntity);
    Task UpdateRange(List<TEntity> updatedEntities);
    Task UpdateEntityExplicit(TEntity entity);
    Task UpdateEntity(TEntity updatedEntity);
    Task UpdateWithCheck(TEntity entity);
    Task DetachEntity(TEntity entity);
    Task Delete(Guid id);
    Task DeleteRang(List<Guid> id);

    Task<DbResponse<List<TEntity>>> FindBy
    (Expression<Func<TEntity, bool>> predicate,
        string includeProperties, PagingParameterModel? paging = null, Expression<Func<TEntity, object>>? orderBy = null);

    Task<DbResponse<List<TEntity>>> FindByAsNoTracking
    (Expression<Func<TEntity, bool>> predicate,
        string includeProperties = "", PagingParameterModel? paging = null,
        Expression<Func<TEntity, object>>? orderBy = null);

    Task<DbResponse<List<TEntity>>> FindBy
    (Expression<Func<TEntity, bool>> predicate, PagingParameterModel? paging = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<DbResponse<List<TEntity>>> FindBy
    (Expression<Func<TEntity, bool>> predicate, PagingParameterModel? paging = null,
        int orderDirection = 1,
        Expression<Func<TEntity, object>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate);

    Task<DbResponse> SaveChangesAsync();

    Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> filter);
    TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> filter, string includeProperties);
    Task<TEntity> FirstOrDefaultAsSplitQuery(Expression<Func<TEntity, bool>> filter, string includeProperties);

    Task BulkInsertAsync(IEnumerable<TEntity> entities);
    Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    Task<(List<TResult> Data, int Count)> FindByWithSelectorPaginated<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, int pageSize, int offset);
    Task<TResult?> FindByIdWithSelector<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector);
    Task<List<TResult>> FindByWithSelector<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
}