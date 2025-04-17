import { useMemo } from "react";

export interface Ticket {
  id: string;
  name: string;
  location: string;
  date: string; // raw date string
  category: string;
  image: string;
}

const mockTickets: Ticket[] = [
  {
    id: "evt001",
    name: "LỄ KỶ NIỆM 2 NĂM HONKAI: STAR RAIL - BUỔI XEM LIVE STAR RAIL 2025 - HỒ CHÍ MINH",
    location: "Lotte Gò Vấp",
    date: "18:00 - 20:00, 03 tháng 05, 2025",
    category: "Concert",
    image: "https://salt.tkbcdn.com/ts/ds/1c/7d/5e/3e6ace30e0a5745dd0a757d2981f84c5.png"
  },
  {
    id: "evt002",
    name: "[SKQT] Kịch Ma 4D : CON QUỶ RỐI - Hết sợ hãi xem kịch Ma",
    location: "Sân Khấu Quốc Thảo",
    date: "19:30 - 22:00, 19 tháng 04, 2025",
    category: "Movie",
    image: "https://salt.tkbcdn.com/ts/ds/53/37/9c/1f49ba697fa7be5253c6a61a1416618e.jpg"
  },
  {
    id: "evt003",
    name: "Attack on Titan Devotion - The Symphony of Freedom",
    location: "Nhà hát Quân đội",
    date: "19:30 - 21:30, 25 tháng 04, 2025",
    category: "Movie",
    image: "https://salt.tkbcdn.com/ts/ds/b4/87/85/fbd8fbcaeca158ad31be21884b29a942.jpg"
  },
  {
    id: "evt004",
    name: "[GARDEN ART] - ART WORKSHOP VẼ TRANH ACRYLIC",
    location: "Sân Khấu Quốc Thảo",
    date: "19:30 - 22:00, 19 tháng 04, 2025",
    category: "Movie",
    image: "https://salt.tkbcdn.com/ts/ds/57/00/c5/6271a558f72c011ec6b8a608ac0811c1.jpg"
  },
];

// Helper để chuyển string date tiếng Việt về Date object
function parseVietnameseDate(dateStr: string): Date | null {
  const match = dateStr.match(/(\d{2}) tháng (\d{2}), (\d{4})/);
  if (!match) return null;

  const [_, day, month, year] = match;
  return new Date(`${year}-${month}-${day}T00:00:00`);
}

export function useGetTickets() {
  const now = new Date();

  const upcomingTickets = useMemo(() => {
    return mockTickets.filter(ticket => {
      const eventDate = parseVietnameseDate(ticket.date);
      return eventDate && eventDate >= now;
    });
  }, []);

  const endedTickets = useMemo(() => {
    return mockTickets.filter(ticket => {
      const eventDate = parseVietnameseDate(ticket.date);
      return eventDate && eventDate < now;
    });
  }, []);

  return { upcomingTickets, endedTickets };
}
