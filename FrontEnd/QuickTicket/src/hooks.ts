import { useCallback, useEffect, useState } from "react";

export interface Event {
  id: string;
  name: string;
  location: string;
  date: string;
  category: string;
  image: string;
  price?: number;
}

export const useGetEvents = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const res = await fetch("../mock/events.json");
        if (!res.ok) throw new Error("Failed to fetch events");
        const data = await res.json();
        setEvents(data);
      } catch (err: any) {
        setError(err.message || "Something went wrong");
      } finally {
        setLoading(false);
      }
    };

    fetchEvents();
  }, []);

  const getEventById = useCallback(
    (id: string): Event | undefined => {
      return events.find((event) => event.id === id);
    },
    [events]
  );

  return { events, loading, error, getEventById };
};
