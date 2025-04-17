import React from 'react';

const categories = [
  '🎵 Âm nhạc', '🎨 Nghệ thuật', '🎤 Hội thảo', '🏃‍♂️ Thể thao', '🎬 Giải trí',
];

const EventCategoriesSection = () => (
  <section className="px-4 space-y-2">
    <h2 className="text-xl font-bold">📂 Chủ đề sự kiện</h2>
    <div className="flex overflow-x-auto gap-3 no-scrollbar">
      {categories.map((cat, index) => (
        <div
          key={index}
          className="px-4 py-2 bg-zinc-100 rounded-full whitespace-nowrap text-sm hover:bg-zinc-200"
        >
          {cat}
        </div>
      ))}
    </div>
  </section>
);

export default EventCategoriesSection;
