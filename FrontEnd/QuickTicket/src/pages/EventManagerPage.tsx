import React, { useState } from 'react';
import useEvents from '@/hooks/useEvents';

const EventManagerPage = () => {
  const {
    events,
    loading,
    error,
    fetchEvents,
    getEventById,
    createEvent,
    updateEvent,
    deleteEvent
  } = useEvents();

  const [selectedEvent, setSelectedEvent] = useState<any>(null);

  const handleGetById = async () => {
    const event = await getEventById(1); // test lấy ID = 1
    setSelectedEvent(event);
  };

  const handleCreate = async () => {
    const newEvent = await createEvent({
      event_name: 'Sự kiện mới',
      event_desc: 'Mô tả sự kiện mới',
      date: '2025-12-31',
      location: 'TP.HCM',
      banner_image: 'https://via.placeholder.com/200',
      category: 'Music',
      quality_ticket: 500
    });
    console.log('Tạo mới:', newEvent);
  };

  const handleUpdate = async () => {
    const updated = await updateEvent(1, {
      event_name: 'Đã cập nhật tên!',
    });
    console.log('Cập nhật:', updated);
  };

  const handleDelete = async () => {
    await deleteEvent(2);
    console.log('Đã xóa event ID 1');
  };

  return (
    <div className="p-4">
      <h1 className="text-xl font-bold mb-2">🧪 Event API Test</h1>

      <div className="flex gap-2 mb-4 flex-wrap">
        <button className="bg-blue-500 text-white px-4 py-2 rounded" onClick={fetchEvents}>
          🔄 Fetch All
        </button>
        <button className="bg-green-500 text-white px-4 py-2 rounded" onClick={handleGetById}>
          🔍 Get ID = 1
        </button>
        <button className="bg-purple-500 text-white px-4 py-2 rounded" onClick={handleCreate}>
          ➕ Create
        </button>
        <button className="bg-yellow-500 text-white px-4 py-2 rounded" onClick={handleUpdate}>
          ✏️ Update ID = 1
        </button>
        <button className="bg-red-500 text-white px-4 py-2 rounded" onClick={handleDelete}>
          ❌ Delete ID = 1
        </button>
      </div>

      {loading && <p>Đang tải dữ liệu...</p>}
      {error && <p className="text-red-500">Lỗi: {error}</p>}

      <h2 className="text-lg font-semibold mt-4">📦 Danh sách sự kiện</h2>
      <ul className="list-disc pl-5">
        {events.map((e) => (
          <li key={e.id}>{e.event_name} - {e.location}</li>
        ))}
      </ul>

      {selectedEvent && (
        <div className="mt-4 p-4 border rounded bg-gray-100">
          <h3 className="font-bold">Chi tiết sự kiện ID 1:</h3>
          <pre>{JSON.stringify(selectedEvent, null, 2)}</pre>
        </div>
      )}
    </div>
  );
};

export default EventManagerPage;
