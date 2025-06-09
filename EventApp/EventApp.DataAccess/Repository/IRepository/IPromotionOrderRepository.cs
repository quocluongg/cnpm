using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IPromotionOrderRepository : IRepository<PromotionOrder>
{
    Task ApplyPromotionToOrderAsync(int promotionId, int orderId);
    Task RemovePromotionFromOrderAsync(int promotionId, int orderId);
}