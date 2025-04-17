import { useState } from "react";

// hooks/useBooking.ts
interface BookingPayload {
  eventId: string;
  userId: string;
  quantity: number;
  customer: {
    fullName: string;
    email: string;
    phone: string;
    note?: string;
  };
}

export const useBooking = () => {
  const [loading, setLoading] = useState(false);

  const bookTicket = async (payload: BookingPayload) => {
    setLoading(true);

    const booking = {
      ...payload,
      bookingTime: new Date().toISOString(),
    };

    try {
      const res = await fetch("/mock/bookings.json", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(booking),
      });

      if (!res.ok) throw new Error("Lỗi khi đặt vé");

      alert("Đặt vé thành công!");
    } catch (err) {
      console.error(err);
      alert("Có lỗi xảy ra khi đặt vé.");
    } finally {
      setLoading(false);
    }
  };

  return { bookTicket, loading };
};
