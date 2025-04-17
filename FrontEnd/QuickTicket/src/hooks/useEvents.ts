import { useState, useEffect } from 'react';
import axios from 'axios';

const API_BASE = 'http://localhost:4000';

// Event type (tùy chỉnh theo schema của bạn)
export interface Event {
  id: string;
  event_name: string;
  event_desc: string;
  date: string;
  location: string;
  banner_image: string;
  category: string;
  quality_ticket: number;
}

const useEvents = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // GET /events
  const fetchEvents = async () => {
    try {
      setLoading(true);
      const res = await axios.get<Event[]>(`${API_BASE}/events`);
      setEvents(res.data);
    } catch (err) {
      handleError(err);
    } finally {
      setLoading(false);
    }
  };

  // GET /events/:id
  const getEventById = async (id: string | number) => {
    try {
      const res = await axios.get<Event>(`${API_BASE}/events/${id}`);
      return res.data;
    } catch (err) {
      handleError(err);
      return null;
    }
  };
  
  // POST /events
  const createEvent = async (eventData: Omit<Event, 'id'>) => {
    try {
      const res = await axios.post<Event>(`${API_BASE}/events`, eventData);
      fetchEvents();
      return res.data;
    } catch (err) {
      handleError(err);
      return null;
    }
  };

  // PUT /events/:id
  const updateEvent = async (id: number, eventData: Partial<Event>) => {
    try {
      const res = await axios.put<Event>(`${API_BASE}/events/${id}`, eventData);
      fetchEvents();
      return res.data;
    } catch (err) {
      handleError(err);
      return null;
    }
  };

  // DELETE /events/:id
  const deleteEvent = async (id: number) => {
    try {
      await axios.delete(`${API_BASE}/events/${id}`);
      fetchEvents();
    } catch (err) {
      handleError(err);
    }
  };

  const handleError = (err: unknown) => {
    if (err instanceof Error) {
      setError(err.message);
    } else {
      setError(String(err));
    }
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  return {
    events,
    loading,
    error,
    fetchEvents,
    getEventById,
    createEvent,
    updateEvent,
    deleteEvent
  };
};

export default useEvents;
