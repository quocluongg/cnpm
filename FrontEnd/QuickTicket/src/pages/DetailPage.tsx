import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import { Button, Header, Page } from 'zmp-ui';
import useEvents from '@/hooks/useEvents';

const DetailPage = () => {
  const { id } = useParams();
  console.log('params id:', id);
  const { loading, error, getEventById } = useEvents();
  const [event, setEvent] = useState<any>(null);

  useEffect(() => {
    if (id) {
      getEventById(Number(id)).then(setEvent);
    }
  }, [id]);

  if (loading) return <p className="p-4">Đang tải chi tiết sự kiện...</p>;
  if (error) return <p className="p-4 text-red-500">Lỗi: {error}</p>;
  if (!event) return <p className="p-4">Không tìm thấy sự kiện</p>;

  return (
    <Page className="flex flex-col items-center pt-[60px]">
      <Header title="Chi tiết sự kiện" />

      <div className="p-4 space-y-4 w-full max-w-md">
        {/* Image */}
        <img
          src={event.banner_image}
          alt={event.event_name}
          className="w-full max-h-[400px] object-cover rounded-lg"
        />

        {/* Name */}
        <h1 className="text-2xl font-bold">{event.event_name}</h1>

        {/* Location */}
        <div className="flex space-x-2 items-center text-gray-600">
          <svg xmlns="http://www.w3.org/2000/svg" className="size-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15 10.5a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M19.5 10.5c0 7.142-7.5 11.25-7.5 11.25S4.5 17.642 4.5 10.5a7.5 7.5 0 1 1 15 0Z" />
          </svg>
          <p>{event.location}</p>
        </div>

        {/* Date */}
        <div className="flex space-x-2 items-center text-gray-600">
          <svg xmlns="http://www.w3.org/2000/svg" className="size-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M12 6v6h4.5m4.5 0a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
          </svg>
          <p>{new Date(event.date).toLocaleString()}</p>
        </div>

        {/* Category */}
        <span className="inline-block mt-2 px-3 py-1 bg-blue-100 text-blue-800 rounded-full">
          {event.category}
        </span>
      </div>

      {/* Buy Ticket */}
      <div className="fixed bottom-0 left-0 right-0 bg-white p-4 shadow z-10">
        <div className="w-full max-w-md mx-auto">
          <Link to={`/booking/${event.id}`}>
            <Button variant="primary" size="large" className="w-full">
              Mua vé
            </Button>
          </Link>
        </div>
      </div>

      {/* Optional: Add Button */}
      <Link to="/add" className="fixed bottom-20 right-4">
        <Button variant="primary" size="large">Add</Button>
      </Link>
    </Page>
  );
};

export default DetailPage;
