import TicketCard from "@/components/shared/TicketCard";
import { useGetEvents } from "@/hooks";
import { createZaloPayOrder } from "@/hooks/useCreateOrder";
import { useState } from "react";
import toast, { Toaster } from "react-hot-toast";
import { Link, useNavigate } from "react-router-dom";
import { Button, Header, Input, Page,Modal } from "zmp-ui";


function TicketBookingPage({eventId,userId}:any) {
  //-- get event need buy
  const {getEventById} = useGetEvents()
  const event = getEventById(eventId)

  const navigate = useNavigate();
  const [ticketQuantity, setTicketQuantity] = useState(1);
  const [orderUrl, setOrderUrl] = useState("");
  const [qrCode, setQrCode] = useState("");
  const [showModal, setShowModal] = useState(false);
  const ticketPrice = event?.price;
  const totalPrice = ticketQuantity * ticketPrice;
  

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const data = new FormData(e.currentTarget);
    const ticketInfo: Record<string, any> = {};
    data.forEach((value, key) => {
      ticketInfo[key] = value;
    });

    const quantity = Number(ticketInfo.ticketQuantity || 1);
    const totalPrice = quantity * ticketPrice;

    const zaloOrderData = {
      fullName: ticketInfo.fullName,
      email: ticketInfo.email,
      phone: ticketInfo.phone,
      quantity,
      amount: totalPrice,
      description: `Thanh toán ${quantity} vé`,
    };

    try {
      const result = await createZaloPayOrder(zaloOrderData);
      if (result.order_url) {
        setOrderUrl(result.order_url);
        setQrCode(result.qr_code)
        console.log(result)
        toast.success("Đã tạo đơn hàng. Vui lòng mở link trong trình duyệt để thanh toán.", {
          duration: 3000,
        });
        setShowModal(true);
      } else {
        toast.error("Không tìm thấy trang thanh toán.");
      }
    } catch (error) {
      console.error("Lỗi tạo đơn hàng:", error);
      toast.error("Đặt vé thất bại!");
    }
  };


  return (
    <Page className="pt-[60px]">
      <Toaster />
      <Header title="Đặt vé sự kiện" />
      <form onSubmit={handleSubmit} className="h-full flex flex-col justify-between">
        <div className="py-2 space-y-2 bg-section">
          <div className="bg-section p-4 grid gap-4">
            <Input name="fullName" label="Họ và tên" placeholder="Nhập họ và tên" required />
            <Input name="email" label="Email" placeholder="abc@example.com" required />
            <Input name="phone" label="Số điện thoại" placeholder="0912345678" required />
            <Input
              name="ticketQuantity"
              type="number"
              label="Số lượng vé"
              placeholder="Nhập số vé"
              min={1}
              value={ticketQuantity}
              onChange={(e) => setTicketQuantity(Number(e.target.value))}
              required
            />
            <Input
              label="Số tiền"
              value={totalPrice.toLocaleString("vi-VN") + " đ"}
              readOnly
            />
          </div>
        </div>

        <div className="p-6 pt-4 bg-section">
          {/* <Link to={`/payment/${qrCode}`}> */}
            <Button htmlType="submit" fullWidth>
              Đặt vé
            </Button>
          {/* </Link> */}
          
          {orderUrl && (
            <div className="mt-4 text-center">
              <p className="mb-2">Mở đường dẫn dưới đây trong Chrome/Safari để thanh toán:</p>
              <a href={orderUrl} target="_blank" rel="noreferrer" className="text-blue-500 underline break-words">
                {orderUrl}
              </a>
              <a href={orderUrl} target="_blank" rel="noreferrer">
                <Button className="mt-2">
                  Mở trang thanh toán
                </Button>
              </a>
            </div>
          )}

          <Modal
            visible={showModal}
            title="Thanh toán ZaloPay"
            description="Đơn hàng đã được tạo, vui lòng thanh toán bằng cách mở đường dẫn dưới đây:"
            onClose={() => setShowModal(false)}
          >
            <div className="text-center space-y-3">
              <a
                href={orderUrl}
                target="_blank"
                rel="noreferrer"
                className="text-blue-500 underline break-words block"
              >
                {orderUrl}
              </a>
              <Button
                onClick={() => {
                  window.open(orderUrl, "_blank");
                }}
                fullWidth
              >
                Mở trang thanh toán
              </Button>
              <Button
                onClick={() => {
                  navigator.clipboard.writeText(orderUrl);
                  toast("Đã sao chép link. Vui lòng mở trình duyệt và dán để thanh toán.");
                }}
                variant="tertiary"
                fullWidth
              >
                Sao chép link
              </Button>
            </div>
          </Modal>

        </div>
      </form>
    </Page>
  );
}

export default TicketBookingPage;
