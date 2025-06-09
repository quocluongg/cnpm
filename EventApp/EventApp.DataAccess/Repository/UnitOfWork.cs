using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;

namespace EventApp.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly EventAppDbContext _context;
    private bool _disposed = false;

    public UnitOfWork(EventAppDbContext context)
    {
        _context = context;
    }

    // Standard repositories
    public IRepository<User> Users => new Repository<User>(_context);
    public IRepository<Event> Events => new Repository<Event>(_context);
    public IRepository<Category> Categories => new Repository<Category>(_context);
    public IRepository<Order> Orders => new Repository<Order>(_context);
    public IRepository<Payment> Payments => new Repository<Payment>(_context);
    public IRepository<TicketType> TicketTypes => new Repository<TicketType>(_context);
    public IRepository<OrderDetail> OrderDetails => new Repository<OrderDetail>(_context);
    public IRepository<Promotion> Promotions => new Repository<Promotion>(_context);
    public IRepository<Extras> Extras => new Repository<Extras>(_context);
    public IRepository<Ticket> Tickets => new Repository<Ticket>(_context);

    // Custom repositories
    public IEventCategoryRepository EventCategories => new EventCategoryRepository(_context);
    public IPromotionOrderRepository PromotionOrders => new PromotionOrderRepository(_context);
    public IExtrasOrderRepository ExtrasOrders => new ExtrasOrderRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}