using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;

namespace EventApp.DataAccess.Repository;

public class EventRepository : Repository<Event>, IEventRepository
{
	private readonly EventAppDbContext _db;
	
	public EventRepository(EventAppDbContext db) : base(db)
	{
		_db = db;
	}

	public void Update(Event @event)
	{
		var objectFromDb = _db.Events.FirstOrDefault(u => u.Id == @event.Id);
		
		if (objectFromDb != null)
		{
			objectFromDb.Name = @event.Name;
			objectFromDb.Date = @event.Date;
			objectFromDb.Location = @event.Location;
			objectFromDb.Price = @event.Price;
			objectFromDb.CategoryId = @event.CategoryId;
			
			if (@event.Banner != null)
			{
				objectFromDb.Banner = @event.Banner;
			}
		}
		else
		{
			_db.Events.Update(@event);
		}
	}
}