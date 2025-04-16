using System.Linq.Expressions;
using EventApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using EventApp.DataAccess.Repository.IRepository;

namespace EventApp.DataAccess.Repository;

public abstract class Repository<T> : IRepository<T> where T : class
{
	private readonly EventAppDbContext _db;
	private readonly DbSet<T> _dbSet;

	protected Repository(EventAppDbContext db)
	{
		_db = db;
		_dbSet = _db.Set<T>();
	}

	public void Add(T entity)
	{
		_dbSet.Add(entity);
	}

	public IEnumerable<T> GetAll(string? includeProperties = null)
	{
		IQueryable<T> query = _dbSet;

		if (!string.IsNullOrEmpty(includeProperties))
		{
			foreach (var includeProp in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp.Trim());
			}
		}

		return query.ToList();
	}

	public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracking = false)
	{
		IQueryable<T> query;
		if (!tracking)
		{
			query = _dbSet.AsNoTracking();
		}
		else
		{
			query = _dbSet;
		}

		query = query.Where(filter);

		if (!string.IsNullOrEmpty(includeProperties))
		{
			foreach (var includeProp in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp.Trim());
			}
		}

		return query.FirstOrDefault();
	}

	public void Remove(T entity)
	{
		_dbSet.Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entity)
	{
		_dbSet.RemoveRange(entity);
	}
}