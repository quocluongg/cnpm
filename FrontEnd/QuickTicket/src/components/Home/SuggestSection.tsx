import { useGetEvents } from '@/hooks'
import useEvents from '@/hooks/useEvents'
import React from 'react'
import { Link } from 'react-router-dom'

const SuggestSection = () => {
  const { events, loading, error } = useEvents()
  console.log(events)

  return (
    <div className="space-y-4">
      {/* Tiêu đề */}
      <div className="px-4 text-[22px] font-semibold">🎉 Dành cho bạn</div>

      {/* Danh sách sự kiện trượt ngang */}
      <div className="overflow-x-auto">
        <div className="flex gap-4 px-4 w-max">
          {events.map((event) => (
            <Link
              key={event.id}
              to={`/events/${event.id}`}
              className="w-64 min-w-[256px] rounded-xl bg-white shadow hover:shadow-lg transition duration-300"
            >
              <img
                src={event.banner_image}
                alt={event.event_name}
                className="w-full h-36 object-cover rounded-t-xl"
              />
              <div className="p-3 space-y-1">
                <div className="font-semibold text-base line-clamp-2">{event.event_name}</div>
                <div className="text-sm text-zinc-500">{event.date}</div>
                <button className="mt-2 px-4 py-1 bg-blue-600 text-white text-sm rounded-full hover:bg-blue-700">
                  Đặt vé
                </button>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </div>
  )
}

export default SuggestSection
