import React from 'react';
import { useGetEvents } from '@/hooks';
import { Swiper } from 'zmp-ui';
import { Link } from 'react-router-dom';

const HotEventsCarousel = () => {
  const { events, loading, error } = useGetEvents();
  const hotEvents = events.slice(0, 5); // có thể thay bằng logic lọc hot thực tế

  if (loading) return <div className="px-4">Đang tải...</div>;
  if (error) return <div className="px-4 text-red-500">Lỗi tải sự kiện</div>;

  return (
    <section className="px-4 space-y-2">
      <h2 className="text-xl font-bold">🔥 Sự kiện nổi bật</h2>
      <Swiper
        // slidesPerView={1.2}
        // spaceBetween={16}
        className="pt-2"
      >
        {hotEvents.map((event) => (
          <Swiper.Slide key={event.id}>
            <Link to={`/event/${event.id}`}>
              <div className="relative rounded-xl overflow-hidden shadow-md">
                <img
                  src={event.image}
                  alt={event.name}
                  className="w-full h-40 object-cover"
                />
                <div className="absolute bottom-0 left-0 right-0 p-3 bg-gradient-to-t from-black/70 to-transparent text-white">
                  <p className="font-bold text-sm">{event.name}</p>
                  <p className="text-xs">{event.date}</p>
                </div>
              </div>
            </Link>
          </Swiper.Slide>
        ))}
      </Swiper>
    </section>
  );
};

export default HotEventsCarousel;
