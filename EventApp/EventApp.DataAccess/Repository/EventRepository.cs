using EventApp.DataAccess.Data;
using EventApp.DataAccess.Repository.IRepository;
using EventApp.Models;

namespace EventApp.DataAccess.Repository;

public class EventRepository : Repository<Event>, IEventRepository
{
	private readonly EventAppDbContext _db;
	
	public EventRepository(EventAppDbContext db) : base(db)
	{
		_db = db;
	}

	public void Update(Event @event)
	{
		var objectFromDb = _db.Events.FirstOrDefault(u => u.Id == @event.Id);
		
		if (objectFromDb != null)
		{
			objectFromDb.TenSuKien = @event.TenSuKien;
			objectFromDb.ThoiGianBatDau = @event.ThoiGianBatDau;
			objectFromDb.ThoiGianKetThuc = @event.ThoiGianKetThuc;
			objectFromDb.NgayTao = @event.NgayTao;
			
			if (@event.HinhAnh != null)
			{
				objectFromDb.HinhAnh = @event.HinhAnh;
			}
			if (@event.MoTa != null)
			{
				objectFromDb.MoTa = @event.MoTa;
			}
		}
		else
		{
			_db.Events.Update(@event);
		}
	}
}