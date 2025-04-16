using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IEventRepository : IRepository<Event>
{
	void Update(Event @event);
}