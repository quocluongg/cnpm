using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.DataAccess.Repository;

public class PromotionOrderRepository : Repository<PromotionOrder>, IPromotionOrderRepository
{
    public PromotionOrderRepository(EventAppDbContext context) : base(context) { }

    public async Task ApplyPromotionToOrderAsync(int promotionId, int orderId)
    {
        await AddAsync(new PromotionOrder { PromotionId = promotionId, OrderId = orderId });
    }

    public async Task RemovePromotionFromOrderAsync(int promotionId, int orderId)
    {
        var po = await _context.PromotionOrders
            .FirstOrDefaultAsync(po => po.PromotionId == promotionId && po.OrderId == orderId);
        if (po != null) Remove(po);
    }
}