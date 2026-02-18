using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TaskyRevamp.Domain;
using TaskyRevamp.Domain.Models;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Domain.Repositeries;
using TaskyRevamp.Dto.GeneralDto;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TaskyRevamp.Infrastructure;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
	private DbContext _context;
	private DbSet<TEntity> _dbSet;
	private IQueryable<TEntity> _queryable;
	private IQueryable<TEntity> _baseQueryable;

	public EfRepository(DbContext context)
	{
		_context = context;
		_dbSet = _context.Set<TEntity>();
		_baseQueryable = _dbSet;
		//if (typeof(TEntity).GetInterfaces().Contains(typeof(ISoftDelete)) && !GetDeleted)
		//{
		//    _queryable = _queryable.Where(query => ((ISoftDelete)query).IsDeleted == false);
		//}

		//if (typeof(TEntity).GetInterfaces().Contains(typeof(IActive)) && !GetInActive)
		//{
		//    _queryable = _queryable.Where(query => ((IActive)query).IsActive == true);
		//}
	}

	public bool GetDeleted { get; set; } = false;
	public bool GetInActive { get; set; } = false;
	public bool IgnoreAutoIncludes { get; set; } = false;

	public async Task<DbResponse<IEnumerable<TEntity>>> All()
	{
		_queryable = _baseQueryable.AsQueryable();
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list?.AsEnumerable());
	}

	public async Task<DbResponse<IEnumerable<TEntity>>> AllAsNoTracking()
	{
		_queryable = _baseQueryable.AsQueryable();
		var list = await _queryable.AsNoTracking().ToListAsync();
		return DbResponse.Ok(list.AsEnumerable());
	}

	public async Task<DbResponse<IEnumerable<TEntity>>> AllAsNoTracking(string includeProperties)
	{
		_queryable = _baseQueryable.AsQueryable();
		_queryable = GetAllIncluding(includeProperties);
		var list = await _queryable.AsNoTracking().ToListAsync();
		return DbResponse.Ok(list.AsEnumerable());
	}

	public async Task<DbResponse<TEntity?>> FindByKey(Guid id)
	{
		_queryable = _baseQueryable.AsQueryable();
		var entity = await _queryable.Where(entity => entity.Id == id).FirstOrDefaultAsync();

		return DbResponse.Ok(entity);
	}

	public async Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate)
	{
		_queryable = _baseQueryable.AsQueryable();

		_queryable = _queryable.Where(predicate);
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);
	}

	public async Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate,
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (IgnoreAutoIncludes)
		{
			_queryable = _queryable.IgnoreAutoIncludes();

		}
		_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);
	}

	public TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate)
	{
		_queryable = _baseQueryable.AsQueryable();
		var entity = _queryable.Where(predicate).AsNoTracking().FirstOrDefaultAsync().Result;
		return entity;

	}

	public TEntity FirstOrDefaultAsNoTracking(Expression<Func<TEntity, bool>> predicate,
		string includeProperties)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (IgnoreAutoIncludes)
		{
			_queryable = _queryable.IgnoreAutoIncludes();

		}
		if (!string.IsNullOrEmpty(includeProperties))
			_queryable = GetAllIncluding(includeProperties);
		var entity = _queryable.Where(predicate).AsNoTracking().FirstOrDefaultAsync().Result;
		return entity;

	}

	public async Task<TEntity> FirstOrDefaultAsSplitQuery(Expression<Func<TEntity, bool>> predicate, string includeProperties)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (IgnoreAutoIncludes)
		{
			_queryable = _queryable.IgnoreAutoIncludes();
		}

		if (!string.IsNullOrEmpty(includeProperties))
		{
			_queryable = GetAllIncluding(includeProperties);
		}

		_queryable = _queryable.AsSplitQuery();

		var entity = await _queryable.Where(predicate).AsNoTracking().FirstOrDefaultAsync();

		return entity;
	}

	public async Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate,
		string includeProperties,
		PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null)
	{
		_queryable = _baseQueryable.AsQueryable();
		_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);
	}

	public async Task<DbResponse<List<TEntity>>> FindWithFilters(Expression<Func<TEntity, bool>> predicate,
		string includeProperties,
		Expression<Func<TEntity, bool>>? filter = null,
		PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (!string.IsNullOrWhiteSpace(includeProperties))
			_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);

		if (filter is not null)
		{
			_queryable = _queryable.Where(filter);
		}

		if (paging is not null)
		{
			ApplyPaging(paging);
		}

		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);
	}

	public async Task<DbResponse<List<TResult>>> FindByWithSelector<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate,
		string includeProperties = null)
	{
		_queryable = _baseQueryable.AsQueryable();
		_queryable = _queryable.AsNoTracking();
		if (!string.IsNullOrEmpty(includeProperties))
			_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);


		var list = await _queryable.Select(selector).ToListAsync();
		return DbResponse.Ok(list);
	}

	public Task<TResult?> FindByIdWithSelector<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector)
	{
		return _baseQueryable
			.AsNoTracking()
			.IgnoreAutoIncludes()
			.Where(x => x.Id == id)
			.Select(selector)
			.FirstOrDefaultAsync();
	}

	public Task<List<TResult>> FindByWithSelector<TResult>(Expression<Func<TEntity, bool>> predicate,
		Expression<Func<TEntity, TResult>> selector)
	{
		return _baseQueryable
			.AsNoTracking()
			.IgnoreAutoIncludes()
			.Where(predicate)
			.Select(selector)
			.ToListAsync();
	}

	public async Task<(List<TResult> Data, int Count)> FindByWithSelectorPaginated<TResult>(Expression<Func<TEntity, bool>> predicate,
		Expression<Func<TEntity, TResult>> selector,
		int pageSize,
		int offset)
	{
		var data = await _baseQueryable
			.AsNoTracking()
			.IgnoreAutoIncludes()
			.Where(predicate)
			.Select(selector)
			.Skip(offset)
			.Take(pageSize)
			.ToListAsync();
		var totalCount = await _baseQueryable
			.AsNoTracking()
			.IgnoreAutoIncludes()
			.CountAsync(predicate);
		return (data, totalCount);
	}

	public async Task<DbResponse<List<TEntity>>> FindWithFiltersAsSplitQuery(Expression<Func<TEntity, bool>> predicate,
		string includeProperties,
		Expression<Func<TEntity, bool>>? filter = null,
		PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null, string orderDir = "asc")
	{
		_queryable = _baseQueryable.AsQueryable();
		_queryable = _queryable.AsSplitQuery();
		_queryable = _queryable.AsNoTracking();
		if (!string.IsNullOrEmpty(includeProperties))
			_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);

		if (filter is not null)
		{
			_queryable = _queryable.Where(filter);
		}

		if (orderBy is not null)
		{
			if (orderDir == "desc")
				_queryable = _queryable.OrderByDescending(orderBy);
			else
				_queryable = _queryable.OrderBy(orderBy);
		}

		if (paging is not null)
		{
			ApplyPaging(paging);
		}

		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);
	}

	public async Task<DbResponse<List<TResult>>> FindWithSelectorFiltersAsSplitQuery<TResult>(Expression<Func<TEntity, bool>> predicate,
		Expression<Func<TEntity, TResult>> selector,
		string includeProperties,
		Expression<Func<TEntity, bool>>? filter = null,
		PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null, string orderDir = "asc")
	{
		_queryable = _baseQueryable.AsQueryable();
		_queryable = _queryable.AsSplitQuery();
		_queryable = _queryable.AsNoTracking();
		_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);

		if (filter is not null)
		{
			_queryable = _queryable.Where(filter);
		}

		if (orderBy is not null)
		{
			if (orderDir == "desc")
				_queryable = _queryable.OrderByDescending(orderBy);
			else
				_queryable = _queryable.OrderBy(orderBy);
		}

		if (paging is not null)
		{
			ApplyPaging(paging);
		}

		var list = await _queryable.Select(selector).ToListAsync();
		return DbResponse.Ok(list);
	}

	public async Task<DbResponse<List<TEntity>>> FindByAsNoTracking(Expression<Func<TEntity, bool>> predicate,
		string includeProperties = "",
		PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (!string.IsNullOrEmpty(includeProperties))
			_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate).AsNoTracking();
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);
	}

	public async Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate,
		PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null,
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (paging is not null)
		{
			ApplyPaging(paging);
		}
		_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);

	}

	private IQueryable<TEntity> GetAllIncluding
		(string includePropertiesString)
	{
		var includeProperties = includePropertiesString.Split(',');
		IQueryable<TEntity> queryable = _queryable;

		var query = includeProperties.Aggregate
			(queryable, (current, includeProperty) => current.Include(includeProperty));
		return query;
	}

	private void ApplyPaging(PagingParameterModel paging)
	{
		if (paging is not null)
		{
			paging.Total = _queryable.Count();
			_queryable = _queryable.Skip(paging.Page * paging.PageSize).Take(paging.PageSize);
		}
	}
	private IQueryable<TEntity> GetAllIncluding
		(params Expression<Func<TEntity, object>>[] includeProperties)
	{
		IQueryable<TEntity> queryable = _queryable;
		var query = includeProperties.Aggregate
			(queryable, (current, includeProperty) => current.Include(includeProperty));
		return query;
	}

	public Task<object> Max(Expression<Func<TEntity, object>> expression, object defaultValue)
	{
		throw new NotImplementedException();
	}

	public async Task<DbResponse<IEnumerable<TEntity>>> GetInclude(
		   Expression<Func<TEntity, bool>>? filter = null,
		   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
		   string includeProperties = "")
	{
		IQueryable<TEntity> queryable = _dbSet;

		if (filter != null)
		{
			queryable = queryable.Where(filter);
		}

		foreach (var includeProperty in includeProperties.Split
			(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		{
			queryable = queryable.Include(includeProperty);
		}

		if (orderBy != null)
		{
			var list = await orderBy(queryable).ToListAsync() as IEnumerable<TEntity>;
			return DbResponse.Ok(list);
		}
		else
		{
			var list = await queryable.ToListAsync() as IEnumerable<TEntity>; ;
			return DbResponse.Ok(list);
		}
	}

	public async Task<DbResponse<IEnumerable<TEntity>>> AllInclude(PagingParameterModel paging = null,
		Expression<Func<TEntity, object>> orderBy = null, string orderDir = "asc", Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
	{

		_queryable = _baseQueryable.AsQueryable();
		if (includeProperties != null)
			_queryable = GetAllIncluding(includeProperties);

		if (predicate != null)
			_queryable = _queryable.Where(predicate);

		if (orderDir == "desc")
			_queryable = _queryable.OrderByDescending(orderBy);
		else
			_queryable = _queryable.OrderBy(orderBy);
		if (paging != null)
			ApplyPaging(paging);

		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list.AsEnumerable());
	}

	public Task<DbResponse<IEnumerable<TEntity>>> AllIncludeDescending(int page = -1, int quantity = 10,
		string orderBy = "",
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		throw new NotImplementedException();
	}

	public Task<DbResponse<IEnumerable<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate,
		PagingParameterModel paging)
	{
		throw new NotImplementedException();
	}


	public Task<DbResponse<TEntity>> FindByKey(Guid id, string includeProperty)
	{
		throw new NotImplementedException();
	}

	public Task<DbResponse<TEntity>> FindByKey(Guid id, Expression<Func<TEntity, object>> predicate)
	{
		throw new NotImplementedException();
	}

	public async Task Insert(TEntity entity)
	{
		await _dbSet.AddAsync(entity);
		await SaveChangesAsync();
		await SaveChangesAsync();
	}

	public async Task InsertRange(IEnumerable<TEntity> entities)
	{
		await _dbSet.AddRangeAsync(entities);
		await SaveChangesAsync();
	}

	public async Task<DbResponse> UpdateResult(TEntity updatedEntity)
	{
		_dbSet.Update(updatedEntity);
		await SaveChangesAsync();
		return DbResponse.Ok();
	}

	public async Task Update(TEntity updatedEntity)
	{
		_dbSet.Update(updatedEntity);
		await SaveChangesAsync();

		_dbSet.Update(updatedEntity);
		await SaveChangesAsync();
	}

	public async Task UpdateRange(List<TEntity> updatedEntities)
	{
		_dbSet.UpdateRange(updatedEntities);
		await SaveChangesAsync();
	}

	public void UpdateNoResult(TEntity updatedEntity)
	{
		_dbSet.Update(updatedEntity);
	}


	public async Task DetachEntity(TEntity entity)
	{
		var entry = _context.Entry(entity);
		if (entry != null)
		{
			entry.State = EntityState.Detached;
		}
	}

	public async Task UpdateEntityExplicit(TEntity entity)
	{
		_dbSet.Attach(entity);
		_context.Entry(entity).State = EntityState.Modified;
		await SaveChangesAsync();
	}

	public async Task UpdateEntity(TEntity updatedEntity)
	{
		var existingEntity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == updatedEntity.Id);
		if (existingEntity != null)
		{
			// Copy the properties from updatedEntity to existingEntity
			_context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
			await SaveChangesAsync();
		}
	}

	public async Task UpdateWithCheck(TEntity entity)
	{
		var tracked = _context.ChangeTracker.Entries<TEntity>().FirstOrDefault(e => e.Entity.Id == entity.Id);
		if (tracked != null)
		{
			// If the entity is already tracked, you can update its state directly
			tracked.CurrentValues.SetValues(entity);
		}
		else
		{
			// If not tracked, attach and mark as modified
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
		await SaveChangesAsync();
	}

	//public async Task<DbResponse> DeleteResult(Guid id)
	//{
	//    _queryable = _baseQueryable.AsQueryable();
	//    var entity = await _queryable.Where(entity => entity.Id == id).FirstOrDefaultAsync();

	//    _dbSet.Remove(entity);
	//    await SaveChangesAsync();
	//    return DbResponse.Ok();

	//}
	public async Task Delete(Guid id)
	{
		_queryable = _baseQueryable.AsQueryable();
		var entity = await _queryable.Where(entity => entity.Id == id).FirstOrDefaultAsync();

		_dbSet.Remove(entity);
		await SaveChangesAsync();


	}
	public async Task DeleteRang(List<Guid> ids)
	{
		_queryable = _baseQueryable.AsQueryable();
		List<TEntity> all = new List<TEntity>();
		//foreach (var item in id.Split(',').ToList())
		//{

		//    var entity = await _queryable.Where(entity => entity.Id == Guid.Parse(item)).FirstOrDefaultAsync();
		//    all.Add(entity);
		//}
		//  var entity = await _queryable.Where(entity =>  id.Split(',').ToList().Contains(entity.Id.ToString()));

		all = await _queryable.Where(entity => ids.Contains(entity.Id)).ToListAsync();

		_dbSet.RemoveRange(all);
		await SaveChangesAsync();


	}
	public async Task DeleteAll()
	{
		await _context.Set<TEntity>().ExecuteDeleteAsync();
	}

	public async Task<DbResponse> SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
		return DbResponse.Ok();
	}

	public async Task<DbResponse<List<TEntity>>> FindBy(Expression<Func<TEntity, bool>> predicate, PagingParameterModel paging = null, int OrderDirection = 1, Expression<Func<TEntity, object>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
	{
		_queryable = _baseQueryable.AsQueryable();
		if (includeProperties is not null)
			_queryable = GetAllIncluding(includeProperties);
		_queryable = _queryable.Where(predicate);
		if (OrderDirection == 1)
			_queryable = _queryable.OrderBy(orderBy);
		else
			_queryable = _queryable.OrderByDescending(orderBy);
		if (paging is not null)
		{
			ApplyPaging(paging);
		}
		var list = await _queryable.ToListAsync();
		return DbResponse.Ok(list);

	}

	public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
	{
		// Use EFCore.BulkExtensions to perform the bulk insert
		await _context.BulkInsertAsync(entities.ToList());
		_context.ChangeTracker.Clear();
	}
	public async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
	{
		// Use EFCore.BulkExtensions to perform the bulk update
		await _context.BulkUpdateAsync(entities.ToList());
		_context.ChangeTracker.Clear();
	}

	public async Task<PagedResult<TEntity>> GetPagedAsync(
	   int pageNumber,
	   int pageSize,
	   Expression<Func<TEntity, bool>>? filter = null,
	   Expression<Func<TEntity, bool>>? searchFilter = null,
	   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
	   string includeProperties = "")
	{
		IQueryable<TEntity> query = _dbSet;

		if (filter != null)
			query = query.Where(filter);

		if (searchFilter != null)
			query = query.Where(searchFilter);

		foreach (var includeProperty in includeProperties.Split(
			new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		{
			query = query.Include(includeProperty);
		}

		if (orderBy != null)
			query = orderBy(query);

		var totalCount = await query.CountAsync();
		var items = await query
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return new PagedResult<TEntity>
		{
			Items = items,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
}