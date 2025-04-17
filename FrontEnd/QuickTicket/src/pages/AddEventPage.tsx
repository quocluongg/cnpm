import React, { useState } from "react";
import { Button, Header, Input, Page } from "zmp-ui";
import toast, { Toaster } from "react-hot-toast";
import { useNavigate } from "react-router-dom";

const AddEventPage = () => {
  const navigate = useNavigate();
  const [eventName, setEventName] = useState("");
  const [eventDescription, setEventDescription] = useState("");
  const [eventDate, setEventDate] = useState("");
  const [eventLocation, setEventLocation] = useState("");
  const [ticketPrice, setTicketPrice] = useState(0);
  const [eventImage, setEventImage] = useState<File | null>(null);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!eventName || !eventDescription || !eventDate || !eventLocation || !ticketPrice || !eventImage) {
      toast.error("Vui lòng điền đầy đủ thông tin!");
      return;
    }

    const formData = new FormData();
    formData.append("name", eventName);
    formData.append("description", eventDescription);
    formData.append("date", eventDate);
    formData.append("location", eventLocation);
    formData.append("ticketPrice", ticketPrice.toString());
    formData.append("image", eventImage);

    try {
      // Gửi dữ liệu tới API để tạo sự kiện mới (giả sử API có endpoint POST /events)
      const response = await fetch("/api/events", {
        method: "POST",
        body: formData,
      });

      if (response.ok) {
        toast.success("Thêm sự kiện thành công!");
        navigate("/admin/events"); // Chuyển hướng về trang danh sách sự kiện (nếu có)
      } else {
        toast.error("Lỗi thêm sự kiện!");
      }
    } catch (error) {
      console.error("Lỗi khi thêm sự kiện:", error);
      toast.error("Đã có lỗi xảy ra khi thêm sự kiện.");
    }
  };

  return (
    <Page className="pt-[60px]">
      <Toaster />
      <Header title="Thêm mới sự kiện" />
      <form onSubmit={handleSubmit} className="p-4 space-y-4">
        <Input
          label="Tên sự kiện"
          value={eventName}
          onChange={(e) => setEventName(e.target.value)}
          required
        />
        <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">Mô tả sự kiện</label>
        <textarea
            value={eventDescription}
            onChange={(e) => setEventDescription(e.target.value)}
            required
            rows={4}
            className="w-full rounded-lg border border-gray-300 p-2 focus:outline-none focus:ring-2 focus:ring-primary focus:border-transparent"
            placeholder="Nhập mô tả sự kiện..."
        />
        </div>
        <Input
          label="Ngày giờ sự kiện"
          type="text"
          value={eventDate}
          onChange={(e) => setEventDate(e.target.value)}
          required
        />
        <Input
          label="Địa điểm"
          value={eventLocation}
          onChange={(e) => setEventLocation(e.target.value)}
          required
        />
        <Input
          label="Giá vé"
          type="number"
          value={ticketPrice}
          onChange={(e) => setTicketPrice(Number(e.target.value))}
          required
        />
        <div>
  <label className="block text-sm font-medium text-gray-700 mb-1">Ảnh sự kiện</label>
  <input
        type="file"
        accept="image/*"
        onChange={(e) => {
        const file = e.target.files?.[0];
        if (file) {
            setEventImage(file);
        }
        }}
        className="block w-full text-sm text-gray-500
        file:mr-4 file:py-2 file:px-4
        file:rounded-lg file:border-0
        file:text-sm file:font-semibold
        file:bg-primary file:text-white
        hover:file:bg-primary/80
        "
    />
    </div>
    {eventImage && (
    <img
        src={URL.createObjectURL(eventImage)}
        alt="Preview"
        className="mt-2 w-full max-h-64 object-cover rounded-lg"
    />
    )}


        <Button htmlType="submit" fullWidth>
          Thêm sự kiện
        </Button>
      </form>
    </Page>
  );
};

export default AddEventPage;
