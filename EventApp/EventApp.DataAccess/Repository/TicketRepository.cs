using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;

namespace EventApp.DataAccess.Repository;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
	private readonly EventAppDbContext _db;
	
	public TicketRepository(EventAppDbContext db) : base(db)
	{
		_db = db;
	}

	public void Update(Ticket ticket)
	{
		var objectFromDb = _db.Tickets.FirstOrDefault(u => u.Id == ticket.Id);
		
		if (objectFromDb != null)
		{
			// objectFromDb.EventId = ticket.EventId;
			// objectFromDb.UserId = ticket.UserId;
		}
		else
		{
			_db.Tickets.Update(ticket);
		}
	}
}