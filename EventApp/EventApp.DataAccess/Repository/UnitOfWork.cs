using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;

namespace EventApp.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly EventAppDbContext _db;
	public ICategoryRepository Category { get; private set; }
	public IEventRepository Event { get; private set; }
	public ITicketRepository Ticket { get; private set; }
	public IUserRepository User { get; private set; }

	public UnitOfWork(EventAppDbContext db)
	{
		_db = db;
		Category = new CategoryRepository(_db);
		Event = new EventRepository(_db);
		Ticket = new TicketRepository(_db);
		User = new UserRepository(_db);
	}

	public void Save()
	{
		_db.SaveChanges();
	}
}