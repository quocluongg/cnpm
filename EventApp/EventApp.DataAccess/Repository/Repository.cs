using System.Linq.Expressions;
using EventApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using EventApp.DataAccess.Repository.IRepository;

namespace EventApp.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
	protected readonly EventAppDbContext _context;
	protected readonly DbSet<T> _dbSet;

	public Repository(EventAppDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

	public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

	public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
		=> await _dbSet.Where(predicate).ToListAsync();

	public async Task<T> AddAsync(T entity)
	{
		var result = await _dbSet.AddAsync(entity);
		return result.Entity;
	}

	public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

	public void Update(T entity) => _dbSet.Update(entity);

	public void Remove(T entity) => _dbSet.Remove(entity);

	public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
}