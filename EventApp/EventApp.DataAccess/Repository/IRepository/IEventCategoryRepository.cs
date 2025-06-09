using EventApp.Models;

namespace EventApp.DataAccess.Repository.IRepository;

public interface IEventCategoryRepository : IRepository<EventCategory>
{
    Task AddEventToCategoryAsync(int eventId, int categoryId);
    Task RemoveEventFromCategoryAsync(int eventId, int categoryId);
    Task<IEnumerable<Category>> GetCategoriesForEventAsync(int eventId);
}