using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;

namespace EventApp.DataAccess.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
	private readonly EventAppDbContext _db;
	
	public UserRepository(EventAppDbContext db) : base(db)
	{
		_db = db;
	}

	public void Update(User user)
	{
		var objectFromDb = _db.Users.FirstOrDefault(u => u.Id == user.Id);
		
		if (objectFromDb != null)
		{
			objectFromDb.Name = user.Name;
			objectFromDb.DoB = user.DoB;
			objectFromDb.PhoneNumber = user.PhoneNumber;
			objectFromDb.Address = user.Address;
		}
		else
		{
			_db.Users.Update(user);
		}
	}
}