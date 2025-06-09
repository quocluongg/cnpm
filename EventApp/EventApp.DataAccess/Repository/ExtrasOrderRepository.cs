using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.DataAccess.Repository;

public class ExtrasOrderRepository : Repository<ExtrasOrder>, IExtrasOrderRepository
{
    public ExtrasOrderRepository(EventAppDbContext context) : base(context) { }

    public async Task AddExtraToOrderAsync(int extraId, int orderId, int quantity)
    {
        await AddAsync(new ExtrasOrder
        {
            ExtrasId = extraId,
            OrderId = orderId,
            Quantity = quantity
        });
    }

    public async Task UpdateExtraQuantityAsync(int extraId, int orderId, int quantity)
    {
        var eo = await _context.ExtrasOrders
            .FirstOrDefaultAsync(eo => eo.ExtrasId == extraId && eo.OrderId == orderId);
        
        if (eo != null)
        {
            eo.Quantity = quantity;
            Update(eo);
        }
    }
}