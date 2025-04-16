using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
	void Update(Category category);
}