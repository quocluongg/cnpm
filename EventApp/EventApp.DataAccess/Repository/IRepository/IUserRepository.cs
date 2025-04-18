using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IUserRepository : IRepository<User>
{
	void Update(User user);
}