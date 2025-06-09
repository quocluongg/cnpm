using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Event> Events { get; }
    IRepository<Category> Categories { get; }
    IRepository<Order> Orders { get; }
    IRepository<Payment> Payments { get; }
    IRepository<TicketType> TicketTypes { get; }
    IRepository<OrderDetail> OrderDetails { get; }
    IRepository<Promotion> Promotions { get; }
    IRepository<Extras> Extras { get; }
    IRepository<Ticket> Tickets { get; }
    
    // Special repositories for junction tables
    IEventCategoryRepository EventCategories { get; }
    IPromotionOrderRepository PromotionOrders { get; }
    IExtrasOrderRepository ExtrasOrders { get; }

    Task<int> CompleteAsync();
}