import React, { useState } from "react";
import { Modal } from "zmp-ui";
import { Ticket } from "../..//hooks/useGetTickets";

const TicketCard = ({ ticket }: { ticket: Ticket }) => {
  const [showQR, setShowQR] = useState(false);

  return (
    <>
      <div className="flex flex-col gap-3 p-3 bg-white rounded-xl shadow-sm border border-gray-100">
        <div className="flex gap-3">
          <img
            src={ticket.image}
            alt={ticket.name}
            className="w-24 h-24 rounded-lg object-cover shrink-0"
          />
          <div className="flex flex-col justify-between flex-1">
            <div>
              <h3 className="text-base font-semibold text-gray-800 line-clamp-2">
                {ticket.name}
              </h3>
              <p className="text-sm text-gray-500 mt-1">{ticket.location}</p>
            </div>
            <div className="text-xs text-gray-400 mt-2">{ticket.date}</div>
          </div>
        </div>
        <button
          onClick={() => setShowQR(true)}
          className="text-primary text-sm self-end mt-2 hover:underline"
        >
          🎫 Xem vé điện tử
        </button>
      </div>

      {/* Modal QR */}
      <Modal
        visible={showQR}
        onClose={() => setShowQR(false)}
        title="Vé điện tử"
        description={ticket.name}
      >
        <div className="flex justify-center py-6">
          <img
            src={`https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=${ticket.id}`}
            alt="QR Code"
            className="rounded"
          />
        </div>
      </Modal>
    </>
  );
};

export default TicketCard;
