using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface ITicketRepository : IRepository<Ticket>
{
	void Update(Ticket ticket);
}