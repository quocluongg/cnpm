using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.DataAccess.Repository;

public class EventCategoryRepository : Repository<EventCategory>, IEventCategoryRepository
{
    public EventCategoryRepository(EventAppDbContext context) : base(context) { }

    public async Task AddEventToCategoryAsync(int eventId, int categoryId)
    {
        await AddAsync(new EventCategory { EventId = eventId, CategoryId = categoryId });
    }

    public async Task RemoveEventFromCategoryAsync(int eventId, int categoryId)
    {
        var ec = await _context.EventCategories
            .FirstOrDefaultAsync(ec => ec.EventId == eventId && ec.CategoryId == categoryId);
        if (ec != null) Remove(ec);
    }

    public async Task<IEnumerable<Category>> GetCategoriesForEventAsync(int eventId)
    {
        return await _context.EventCategories
            .Where(ec => ec.EventId == eventId)
            .Select(ec => ec.Category)
            .ToListAsync();
    }
}