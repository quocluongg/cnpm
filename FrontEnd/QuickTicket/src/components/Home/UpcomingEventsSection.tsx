import React from 'react';
import { useGetEvents } from '@/hooks';
import { Link } from 'react-router-dom';

const UpcomingEventsSection = () => {
  const { events } = useGetEvents();
  const upcoming = events.slice(0, 4); // tuỳ chọn lọc logic

  return (
    <section className="px-4 space-y-4">
      <h2 className="text-xl font-bold">⏰ Sắp diễn ra</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        {upcoming.map((event) => (
          <Link key={event.id} to={`/event/${event.id}`}>
            <div className="p-4 rounded-xl bg-zinc-100 hover:bg-zinc-200 transition">
              <p className="font-semibold">{event.name}</p>
              <p className="text-sm text-zinc-500">{event.date}</p>
            </div>
          </Link>
        ))}
      </div>
    </section>
  );
};

export default UpcomingEventsSection;
