using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;

namespace EventApp.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
	private readonly EventAppDbContext _db;
	
	public CategoryRepository(EventAppDbContext db) : base(db)
	{
		_db = db;
	}

	public void Update(Category category)
	{
		var objectFromDb = _db.Categories.FirstOrDefault(u => u.Id == category.Id);
		
		if (objectFromDb != null)
		{
			objectFromDb.TenDanhMuc = category.TenDanhMuc;
		}
		else
		{
			_db.Categories.Update(category);
		}
	}
}