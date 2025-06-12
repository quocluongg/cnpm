using System.Linq.Expressions;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
	Task<T?> GetByIdAsync(int id);
	Task<IEnumerable<T>> GetAllAsync();
	Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
	Task<T> AddAsync(T entity);
	Task AddRangeAsync(IEnumerable<T> entities);
	void Update(T entity);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
}