import React from 'react'

const EventCard = ({ imageSrc, title, date }) => (
  <div className="w-48 min-w-[12rem] bg-white dark:bg-zinc-900 rounded-2xl shadow-md overflow-hidden">
    <img
      src={imageSrc}
      alt={title}
      className="w-full h-28 object-cover"
    />
    <div className="p-3 space-y-1">
      <h3 className="text-base font-semibold text-zinc-800 dark:text-white line-clamp-2">
        {title}
      </h3>
      <p className="text-xs text-zinc-500 dark:text-zinc-400">{date}</p>
    </div>
  </div>
);


export default EventCard
