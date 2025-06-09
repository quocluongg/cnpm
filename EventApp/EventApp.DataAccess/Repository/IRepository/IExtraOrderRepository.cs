using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IExtrasOrderRepository : IRepository<ExtrasOrder>
{
    Task AddExtraToOrderAsync(int extraId, int orderId, int quantity);
    Task UpdateExtraQuantityAsync(int extraId, int orderId, int quantity);
}