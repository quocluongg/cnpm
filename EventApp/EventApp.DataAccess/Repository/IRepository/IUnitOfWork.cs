namespace EventApp.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
	ICategoryRepository Category { get; }
	IEventRepository Event { get; }
	ITicketRepository Ticket { get; }
	IUserRepository User { get; }
	
	void Save();
}